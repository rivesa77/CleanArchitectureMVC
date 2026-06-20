// <copyright file="PersonRepository.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace MVCPrueba1.Data.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using MVCPrueba1.Entities;
    using MVCPrueba1.Logic.Repositories;

    internal class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public PersonRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<bool> ExistsByDniAsync(string dni)
        {
            string normalizedDni = dni?.Trim().ToUpperInvariant() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(normalizedDni))
            {
                return false;
            }

            return await this.applicationDbContext.Persons
                .AnyAsync(p => p.DNI == normalizedDni)
                .ConfigureAwait(false);
        }

        public async Task<bool> ExistsByDniAndIdAsync(string dni, Guid id)
        {
            string normalizedDni = dni?.Trim().ToUpperInvariant() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(normalizedDni))
            {
                return false;
            }

            return await this.applicationDbContext.Persons
                .AnyAsync(p => p.DNI == normalizedDni && p.Id != id)
                .ConfigureAwait(false);
        }

        public async Task AddAsync(PersonEntity personEntity)
        {
            await this.applicationDbContext.AddAsync(personEntity).ConfigureAwait(false);
            await this.applicationDbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<PersonEntity>> GetByUserIdAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return [];
            }

            return await this.applicationDbContext.Persons
                .Where(p => p.UserId == userId)
                .AsNoTracking()
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
            this.applicationDbContext.Persons.Update(personEntity);

            await this.applicationDbContext.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }

        public async Task<bool> DeletePersonAsync(PersonEntity personEntity)
        {
            this.applicationDbContext.Persons.Remove(personEntity);

            await this.applicationDbContext.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }
    }
}