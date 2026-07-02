// <copyright file="UpdatePersonUseCaseTests.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.CleanArchitectureMVC.Application.Tests.UseCases.Persons.Updates
{
    using FluentAssertions;
    using Moq;
    using Ricardo.CleanArchitectureMVC.Application.Converters.PersonsViewModel.ToPersonEntity;
    using Ricardo.CleanArchitectureMVC.Application.Models;
    using Ricardo.CleanArchitectureMVC.Application.Repositories;
    using Ricardo.CleanArchitectureMVC.Application.Tests.Constants;
    using Ricardo.CleanArchitectureMVC.Application.Tests.Models.Validations;
    using Ricardo.CleanArchitectureMVC.Application.UseCases.Persons.Updates;
    using Ricardo.CleanArchitectureMVC.Application.UserInfo;
    using Ricardo.CommonLibraries.Extensions.Tests.Mocks;
    using ROP;

    [TestClass]
    [TestCategory("UpdatePersonUseCase")]
    public class UpdatePersonUseCaseTests
    {
        private Mock<IPersonRepository> mockPersonRepository;
        private Mock<IPersonsViewModelToPersonEntityConverter> mockConverter;
        private Mock<IPersonUserDetails> mockPersonUserDetails;

        [TestInitialize]
        public void Initialize()
        {
            this.mockPersonRepository = new Mock<IPersonRepository>(MockBehavior.Strict);
            this.mockConverter = new Mock<IPersonsViewModelToPersonEntityConverter>(MockBehavior.Strict);
            this.mockPersonUserDetails = new Mock<IPersonUserDetails>(MockBehavior.Strict);
        }

        [TestCleanup]
        public void Verify()
        {
            this.mockPersonRepository.VerifyAllAndOtherCalls();
            this.mockConverter.VerifyAllAndOtherCalls();
            this.mockPersonUserDetails.VerifyAllAndOtherCalls();
        }

        [TestMethod]
        public async Task Execute_WhenPersonIdIsEmpty_ReturnsOperationErrorAndDoesNotCallCollaborators()
        {
            // Arrange.
            PersonViewModel personViewModel = new PersonViewModel()
            {
                DNI = PersonConstants.Dni,
                Email = PersonConstants.Email,
                Name = PersonConstants.Name,
                Phone = PersonConstants.Phone,
                Id = Guid.Empty,
            };

            UpdatePersonUseCase updatePersonUseCase = this.CreateUseCase();

            // Act.
            Result<bool> result = await updatePersonUseCase.Execute(personViewModel).ConfigureAwait(false);

            // Assert.
            ValidateFailure(result, "The person can't be updated");
        }

        [TestMethod]
        [DataRow(PersonViewModelField.Dni, null, ValidatorConstantTests.DniRequiredMessage)]
        [DataRow(PersonViewModelField.Name, null, ValidatorConstantTests.NameRequiredMessage)]
        [DataRow(PersonViewModelField.Phone, "12345678A", ValidatorConstantTests.PhoneInvalidMessage)]
        [DataRow(PersonViewModelField.Email, "invalid-email", ValidatorConstantTests.EmailInvalidMessage)]
        public async Task Execute_WhenPersonDataIsInvalid_ReturnsValidationErrorAndDoesNotCallCollaborators(
            PersonViewModelField propertyName,
            string value,
            string expectedMessage)
        {
            // Arrange.
            PersonViewModel personViewModel = PersonViewModelHelper.SetValueInProperty(propertyName, value);

            UpdatePersonUseCase updatePersonUseCase = this.CreateUseCase();

            // Act.
            Result<bool> result = await updatePersonUseCase.Execute(personViewModel).ConfigureAwait(false);

            // Assert.
            ValidateFailure(result, expectedMessage);
        }

        private static void ValidateFailure(Result<bool> result, string expectedMessage)
        {
            result.Value
                .Should()
                .BeFalse();

            result.Errors
                .Should()
                .ContainSingle();

            result.Errors.First().Message
                .Should()
                .Be(expectedMessage);
        }

        private UpdatePersonUseCase CreateUseCase()
        {
            return new UpdatePersonUseCase(
                this.mockPersonRepository.Object,
                this.mockConverter.Object,
                this.mockPersonUserDetails.Object);
        }
    }
}