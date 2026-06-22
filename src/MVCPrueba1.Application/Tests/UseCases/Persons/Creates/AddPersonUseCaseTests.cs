// <copyright file="AddPersonUseCaseTests.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Application.Tests.UseCases.Persons.Creates
{
    using Application.Tests.Constants;
    using FluentAssertions;
    using Moq;
    using MVCPrueba1.Application.Converter.PersonsViewModel.ToPersonEntity;
    using MVCPrueba1.Application.Models;
    using MVCPrueba1.Application.Repositories;
    using MVCPrueba1.Application.UseCases.Persons.Creates;
    using MVCPrueba1.Domain.Entities;
    using Ricardo.CommonLibraries.Extensions.Tests.Mocks;
    using ROP;

    [TestClass]
    public class AddPersonUseCaseTests
    {
        private const string DniRequiredMessage = "Person DNI is required";
        private const string DniAlreadyExistMessage = "Person DNI Already Exist";
        private const string ConverterErrorMessage = "Conversion from PersonViewModel to PersonEntity failed, PersonEntity is null";

        private static readonly PersonViewModel PersonViewModel = new PersonViewModel()
        {
            DNI = PersonViewModelConstants.Dni,
            Email = PersonViewModelConstants.Email,
            Name = PersonViewModelConstants.Name,
            Phone = PersonViewModelConstants.Phone,
        };

        private Mock<IPersonRepository> mockPersonRepository;
        private Mock<IPersonsViewModelToPersonEntityConverter> mockConverter;

        [TestInitialize]
        public void Initialize()
        {
            this.mockPersonRepository = new(MockBehavior.Strict);
            this.mockConverter = new(MockBehavior.Strict);
        }

        [TestCleanup]
        public void Verify()
        {
            this.mockPersonRepository.VerifyAllAndOtherCalls();
            this.mockConverter.VerifyAllAndOtherCalls();
        }

        [TestMethod]
        public async Task Execute_WhenDniIsEmpty_ReturnsMessageErrorAndDoesNotCallRepository()
        {
            // Arrange.
            this.mockPersonRepository
                .Setup(m => m.ExistsByDniAsync(It.IsAny<string>()))
                .ReturnsAsync(false)
                .Verifiable(Times.Never());

            this.mockPersonRepository
                .Setup(m => m.AddAsync(It.IsAny<PersonEntity>()))
                .Verifiable(Times.Never);

            AddPersonUseCase addPersonUseCase = new AddPersonUseCase(
                personRepository: this.mockPersonRepository.Object,
                converter: this.mockConverter.Object);

            // Act.
            Result<bool> result = await addPersonUseCase
                .Execute(new PersonViewModel())
                .ConfigureAwait(false);

            // Assert.
            ValidateExpectResutl(result, DniRequiredMessage);
        }

        [TestMethod]
        public async Task Execute_WhenDniExist_ReturnsMessageErrorAndCallRepository()
        {
            // Arrange..
            this.InitializeNonValidMocks(PersonViewModel.DNI, true);

            AddPersonUseCase addPersonUseCase = new AddPersonUseCase(
                personRepository: this.mockPersonRepository.Object,
                converter: this.mockConverter.Object);

            // Act.
            Result<bool> result = await addPersonUseCase
                .Execute(PersonViewModel)
                .ConfigureAwait(false);

            // Assert.
            ValidateExpectResutl(result, DniAlreadyExistMessage);
        }

        [TestMethod]
        public async Task Execute_WhenConverterReturnNull_ReturnsMessageErrorAndCallRepository()
        {
            // Arrange.
            this.InitializeNonValidMocks(PersonViewModel.DNI, false);

            this.mockConverter.Setup(m => m.Convert(PersonViewModel))
                .Returns(default(PersonEntity))
                .Verifiable(Times.Once());

            AddPersonUseCase addPersonUseCase = new AddPersonUseCase(
                personRepository: this.mockPersonRepository.Object,
                converter: this.mockConverter.Object);

            // Act.
            Result<bool> result = await addPersonUseCase
                .Execute(PersonViewModel)
                .ConfigureAwait(false);

            // Assert.
            ValidateExpectResutl(result, ConverterErrorMessage);
        }

        [TestMethod]
        public async Task Execute_WhenValidPersonViewModel_ReturnsExpectedResult()
        {
            // Arrange.
            this.mockPersonRepository
                .Setup(m => m.ExistsByDniAsync(PersonViewModel.DNI))
                .ReturnsAsync(false)
                .Verifiable(Times.Once());

            PersonEntity personEntity = new PersonEntity()
            {
                DNI = PersonViewModel.DNI,
                Email = PersonViewModel.Email,
                Name = PersonViewModel.Name,
                Phone = PersonViewModel.Phone,
                UserId = string.Empty,
            };

            this.mockConverter.Setup(m => m.Convert(PersonViewModel))
                .Returns(personEntity)
                .Verifiable(Times.Once());

            this.mockPersonRepository
                .Setup(m => m.AddAsync(personEntity))
                .ReturnsAsync(true)
                .Verifiable(Times.Once());

            AddPersonUseCase addPersonUseCase = new AddPersonUseCase(
                personRepository: this.mockPersonRepository.Object,
                converter: this.mockConverter.Object);

            // Act.
            Result<bool> expectedResult = await addPersonUseCase
                .Execute(PersonViewModel)
                .ConfigureAwait(false);

            // Assert.
            expectedResult.Value
                .Should()
                .BeTrue();

            expectedResult.Errors
                .Should()
                .BeEmpty();
        }

        private static void ValidateExpectResutl(Result<bool> result, string messageError)
        {
            result.Value
                .Should()
                .BeFalse();

            result.Errors
                .Should()
                .HaveCount(1);

            result.Errors.First().Message
                .Should()
                .BeEquivalentTo(messageError);
        }

        private void InitializeNonValidMocks(string dni, bool returnResult)
        {
            this.mockPersonRepository
                .Setup(m => m.ExistsByDniAsync(dni))
                .ReturnsAsync(returnResult)
                .Verifiable(Times.Once());

            this.mockPersonRepository
                .Setup(m => m.AddAsync(It.IsAny<PersonEntity>()))
                .Verifiable(Times.Never);
        }
    }
}