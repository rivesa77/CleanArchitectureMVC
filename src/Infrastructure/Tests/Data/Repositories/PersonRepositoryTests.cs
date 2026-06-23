// <copyright file="PersonRepositoryTests.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Infrastructure.Tests.Data.Repositories
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Ricardo.MVCPrueba1.Domain.Entities;
    using Ricardo.MVCPrueba1.Infrastructure.Data;
    using Ricardo.MVCPrueba1.Infrastructure.Data.Repositories;
    using Ricardo.MVCPrueba1.Infrastructure.Tests.Constants;

    [TestClass]
    [TestCategory("Infrastructure.PersonRepository")]
    public class PersonRepositoryTests
    {
        private static readonly PersonEntity PersonEntity = new PersonEntity()
        {
            UserId = PersonEntityConstants.UserId,
            Name = PersonEntityConstants.Name,
            DNI = PersonEntityConstants.Dni,
            Email = PersonEntityConstants.Email,
            Id = Guid.NewGuid(),
            Phone = PersonEntityConstants.Phone,
        };

        [TestMethod]
        [DataRow(PersonEntityConstants.Dni, true)]
        [DataRow(PersonEntityConstants.AnotherDni, false)]
        [DataRow(default, false)]
        [DataRow("", false)]
        public async Task SaveChangesAsync_WithValues_ReturnsExpectedResult(
            string dni,
            bool expectedResult)
        {
            ApplicationDbContext inMemoryDb = MemoryDbContext.GetInMemoryDbContext();

            inMemoryDb.Persons.Add(PersonEntity);
            await inMemoryDb.SaveChangesAsync();

            PersonRepository personRepository = new PersonRepository(inMemoryDb);

            bool result = await personRepository
                .ExistsByDniAsync(dni)
                .ConfigureAwait(false);

            result
                .Should()
                .Be(expectedResult);
        }
    }
}