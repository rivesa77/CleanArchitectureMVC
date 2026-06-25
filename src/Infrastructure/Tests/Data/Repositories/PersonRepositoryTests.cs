// <copyright file="PersonRepositoryTests.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Infrastructure.Tests.Data.Repositories
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Ricardo.MVCPrueba1.Application.Models;
    using Ricardo.MVCPrueba1.Application.Repositories;
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

        [TestMethod]
        public async Task SearchByUserIdAsync_WhenSearchByName_ReturnsFilteredPageAndTotal()
        {
            ApplicationDbContext inMemoryDb = MemoryDbContext.GetInMemoryDbContext();

            inMemoryDb.Persons.AddRange(
                CreatePerson("Alice", "11111111A", PersonEntityConstants.UserId),
                CreatePerson("Alicia", "22222222B", PersonEntityConstants.UserId),
                CreatePerson("Bob", "33333333C", PersonEntityConstants.UserId),
                CreatePerson("Alice Other", "44444444D", "OtherUserId"));
            await inMemoryDb.SaveChangesAsync();

            PersonRepository personRepository = new PersonRepository(inMemoryDb);

            (IEnumerable<PersonEntity> persons, int totalItems) = await personRepository
                .SearchByUserIdAsync(new PersonSearchQuery()
                {
                    UserId = PersonEntityConstants.UserId,
                    SearchField = PersonSearchField.Name,
                    SearchTerm = "ali",
                    PageNumber = 1,
                    PageSize = 1,
                })
                .ConfigureAwait(false);

            totalItems
                .Should()
                .Be(2);

            persons
                .Should()
                .ContainSingle()
                .Which.Name
                .Should()
                .Be("Alice");
        }

        private static PersonEntity CreatePerson(string name, string dni, string userId)
        {
            return new PersonEntity()
            {
                UserId = userId,
                Name = name,
                DNI = dni,
                Email = $"{name.Replace(" ", string.Empty)}@email.com",
                Id = Guid.NewGuid(),
                Phone = PersonEntityConstants.Phone,
            };
        }
    }
}
