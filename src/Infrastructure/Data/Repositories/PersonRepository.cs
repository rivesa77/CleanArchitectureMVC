// <copyright file="PersonRepository.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Infrastructure.Data.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Ricardo.MVCPrueba1.Application.Models;
    using Ricardo.MVCPrueba1.Application.Repositories;
    using Ricardo.MVCPrueba1.Domain.Entities;

    internal class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public PersonRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<bool> ExistsByDniAsync(string dni)
        {
            bool isNormalizedDni = NormalizeDni(ref dni);

            if (!isNormalizedDni)
            {
                return false;
            }

            return await this.applicationDbContext.Persons
                .AsNoTracking()
                .AnyAsync(p => p.DNI == dni)
                .ConfigureAwait(false);
        }

        public async Task<bool> ExistsByDniAndIdAsync(string dni, Guid id)
        {
            bool isNormalizedDni = NormalizeDni(ref dni);

            if (!isNormalizedDni)
            {
                return false;
            }

            return await this.applicationDbContext.Persons
                .AsNoTracking()
                .AnyAsync(p => p.DNI == dni && p.Id != id)
                .ConfigureAwait(false);
        }

        public async Task<bool> AddAsync(PersonEntity personEntity)
        {
            if (personEntity is null)
            {
                return false;
            }

            await this.applicationDbContext.AddAsync(personEntity).ConfigureAwait(false);
            int result = await this.applicationDbContext.SaveChangesAsync().ConfigureAwait(false);

            return result > 0;
        }

        public async Task<IEnumerable<PersonEntity>> GetByUserIdAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return [];
            }

            return await this.applicationDbContext.Persons
                .AsNoTracking()
                .Where(p => p.UserId == userId)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<(IEnumerable<PersonEntity> Persons, int TotalItems)> SearchByUserIdAsync(PersonSearchQuery personSearchQuery)
        {
            if (personSearchQuery is null || string.IsNullOrWhiteSpace(personSearchQuery.UserId))
            {
                return ([], 0);
            }

            int pageNumber = personSearchQuery.PageNumber < 1 ? 1 : personSearchQuery.PageNumber;

            int pageSize = personSearchQuery.PageSize < 1 ? 5 : personSearchQuery.PageSize;

            IQueryable<PersonEntity> query = this.applicationDbContext.Persons
                .AsNoTracking()
                .Where(p => p.UserId == personSearchQuery.UserId);

            query = ApplySearch(
                query,
                personSearchQuery.SearchField,
                personSearchQuery.SearchTerm);

            int totalItems = await query.CountAsync().ConfigureAwait(false);

            IEnumerable<PersonEntity> persons = await query
                .OrderBy(p => p.Name)
                .ThenBy(p => p.DNI)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync()
                .ConfigureAwait(false);

            return (persons, totalItems);
        }

        public async Task<PersonEntity> GetByIdAndUserIdAsync(Guid id, string userId)
        {
            if (id == Guid.Empty || string.IsNullOrWhiteSpace(userId))
            {
                return default;
            }

            return await this.applicationDbContext.Persons
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == id && p.UserId == userId)
                .ConfigureAwait(false);
        }

        public async Task<bool> UpdatePersonAsync(PersonEntity personEntity)
        {
            if (personEntity is null)
            {
                return false;
            }

            this.applicationDbContext.Persons.Update(personEntity);

            await this.applicationDbContext.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }

        public async Task<bool> DeletePersonAsync(PersonEntity personEntity)
        {
            if (personEntity is null)
            {
                return false;
            }

            this.applicationDbContext.Persons.Remove(personEntity);

            await this.applicationDbContext.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }

        private static IQueryable<PersonEntity> ApplySearch(
            IQueryable<PersonEntity> query,
            PersonSearchField searchField,
            string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return query;
            }

            string normalizedSearchTerm = searchTerm.Trim().ToUpper();

            return searchField switch
            {
                PersonSearchField.Dni => query.Where(p => p.DNI.ToUpper().Contains(normalizedSearchTerm)),
                PersonSearchField.Name => query.Where(p => p.Name != null && p.Name.ToUpper().Contains(normalizedSearchTerm)),
                PersonSearchField.Email => query.Where(p => p.Email != null && p.Email.ToUpper().Contains(normalizedSearchTerm)),
                PersonSearchField.Phone => query.Where(p => p.Phone != null && p.Phone.ToUpper().Contains(normalizedSearchTerm)),
                _ => query.Where(p =>
                    p.DNI.ToUpper().Contains(normalizedSearchTerm)
                    || (p.Name != null && p.Name.ToUpper().Contains(normalizedSearchTerm))
                    || (p.Email != null && p.Email.ToUpper().Contains(normalizedSearchTerm))
                    || (p.Phone != null && p.Phone.ToUpper().Contains(normalizedSearchTerm))),
            };
        }

        private static bool NormalizeDni(ref string dni)
        {
            string normalizedDni = dni?.Trim().ToUpperInvariant() ?? default;

            if (string.IsNullOrWhiteSpace(normalizedDni))
            {
                return false;
            }

            return true;
        }
    }
}