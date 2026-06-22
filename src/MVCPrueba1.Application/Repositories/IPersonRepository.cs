// <copyright file="IPersonRepository.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace MVCPrueba1.Application.Repositories
{
    using MVCPrueba1.Domain.Entities;

    /// <summary>
    ///     Define las operaciones de persistencia necesarias para trabajar con personas.
    /// </summary>
    /// <remarks>
    ///     La interfaz vive donde se consume; la implementacion vive donde se ejecuta el detalle tecnico. Si se cambia la implementacion,
    ///     no se cambia la interfaz. La interfaz es un contrato que define lo que se espera de la implementacion.
    /// </remarks>
    public interface IPersonRepository
    {
        Task<bool> ExistsByDniAsync(string dni);

        Task<bool> AddAsync(PersonEntity personEntity);

        Task<IEnumerable<PersonEntity>> GetByUserIdAsync(string userId);

        Task<PersonEntity> GetByIdAndUserIdAsync(Guid id, string userId);

        Task<bool> ExistsByDniAndIdAsync(string dni, Guid id);

        Task<bool> UpdatePersonAsync(PersonEntity personEntity);

        Task<bool> DeletePersonAsync(PersonEntity personEntity);
    }
}