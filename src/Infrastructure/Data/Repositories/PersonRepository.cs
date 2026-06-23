// <copyright file="PersonRepository.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Infrastructure.Data.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Ricardo.Application.Repositories;
    using Ricardo.Domain.Entities;

    internal class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public PersonRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<bool> ExistsByDniAsync(string dni)
        {
            bool isNormalizedDni = this.NormalizeDni(ref dni);

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
            bool isNormalizedDni = this.NormalizeDni(ref dni);

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

        private bool NormalizeDni(ref string dni)
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