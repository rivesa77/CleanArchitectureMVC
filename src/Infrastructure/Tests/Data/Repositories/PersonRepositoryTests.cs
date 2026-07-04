// <copyright file="PersonRepositoryTests.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.CleanArchitectureMVC.Infrastructure.Tests.Data.Repositories
{
    using FluentAssertions;
    using Microsoft.Extensions.Logging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Ricardo.CleanArchitectureMVC.Application.Models;
    using Ricardo.CleanArchitectureMVC.Application.Repositories;
    using Ricardo.CleanArchitectureMVC.Domain.Entities;
    using Ricardo.CleanArchitectureMVC.Infrastructure.Data;
    using Ricardo.CleanArchitectureMVC.Infrastructure.Data.Repositories;
    using Ricardo.CleanArchitectureMVC.Infrastructure.Tests.Constants;

    [TestClass]
    [TestCategory("Infrastructure.PersonRepository")]
    public class PersonRepositoryTests
    {
        private static readonly Mock<ILogger<IPersonRepository>> MockLogger = new Mock<ILogger<IPersonRepository>>();

        private static readonly PersonEntity PersonOne = new PersonEntity()
        {
            Name = "Francisco",
            DNI = "11111111A",
            Email = $"francisco@email.com",
            UserId = PersonEntityConstants.UserId,
            Phone = PersonEntityConstants.Phone,
            Id = Guid.NewGuid(),
        };

        private static readonly PersonEntity PersonTwo = new PersonEntity()
        {
            Name = "Mario",
            DNI = "22222222B",
            Email = $"mario@email.com",
            UserId = PersonEntityConstants.UserId,
            Phone = PersonEntityConstants.Phone,
            Id = Guid.NewGuid(),
        };

        private static readonly PersonEntity PersonThree = new PersonEntity()
        {
            Name = "Maria",
            DNI = "33333333C",
            Email = $"maria@email.com",
            UserId = PersonEntityConstants.UserId,
            Phone = PersonEntityConstants.Phone,
            Id = Guid.NewGuid(),
        };

        private static readonly PersonEntity PersonFour = new PersonEntity()
        {
            Name = "Marta",
            DNI = "44444444D",
            Email = $"marta@email.com",
            UserId = "OtherUserId",
            Phone = PersonEntityConstants.Phone,
            Id = Guid.NewGuid(),
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

            PersonEntity personEntity = new PersonEntity()
            {
                UserId = PersonEntityConstants.UserId,
                Name = PersonEntityConstants.Name,
                DNI = PersonEntityConstants.Dni,
                Email = PersonEntityConstants.Email,
                Id = Guid.NewGuid(),
                Phone = PersonEntityConstants.Phone,
            };

            inMemoryDb.Persons.Add(personEntity);

            await inMemoryDb.SaveChangesAsync();

            PersonRepository personRepository = new PersonRepository(inMemoryDb, MockLogger.Object);

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
                PersonOne,
                PersonThree,
                PersonTwo,
                PersonFour);

            await inMemoryDb.SaveChangesAsync();

            PersonRepository personRepository = new PersonRepository(inMemoryDb, MockLogger.Object);

            PersonSearchQuery personSearchQuery = new PersonSearchQuery()
            {
                UserId = PersonEntityConstants.UserId,
                SearchField = PersonSearchField.Name,
                SearchTerm = "Mari",
                PageNumber = 1,
                PageSize = 1,
            };

            (IEnumerable<PersonEntity> persons, int totalItems) = await personRepository
                .SearchByUserIdAsync(personSearchQuery).ConfigureAwait(false);

            totalItems
                .Should()
                .Be(2);

            persons
                .Should()
                .ContainSingle()
                .Which.Name
                .Should()
                .Be("Maria");
        }

        [TestMethod]
        public async Task SearchByUserIdAsync_WhenSortByDniDescending_ReturnsExpectedOrder()
        {
            ApplicationDbContext inMemoryDb = MemoryDbContext.GetInMemoryDbContext();

            inMemoryDb.Persons.AddRange(
                PersonOne,
                PersonThree,
                PersonTwo);

            await inMemoryDb.SaveChangesAsync();

            PersonRepository personRepository = new PersonRepository(inMemoryDb, MockLogger.Object);

            PersonSearchQuery personSearchQuery = new PersonSearchQuery()
            {
                UserId = PersonEntityConstants.UserId,
                SortField = PersonSortField.Dni,
                SortDirection = PersonSortDirection.Descending,
                PageNumber = 1,
                PageSize = 3,
            };

            (IEnumerable<PersonEntity> persons, int totalItems) = await personRepository
                .SearchByUserIdAsync(new PersonSearchQuery()
                {
                    UserId = PersonEntityConstants.UserId,
                    SortField = PersonSortField.Dni,
                    SortDirection = PersonSortDirection.Descending,
                    PageNumber = 1,
                    PageSize = 3,
                })
                .ConfigureAwait(false);

            totalItems
                .Should()
                .Be(3);

            persons
                .Select(person => person.DNI)
                .Should()
                .ContainInOrder("33333333C", "22222222B", "11111111A");
        }
    }
}