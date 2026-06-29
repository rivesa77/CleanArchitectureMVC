// <copyright file="UserIdConverterTests.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.Tests.Converter.PersonsViewModel.ToPersonEntity.Properties
{
    using FluentAssertions;
    using Moq;
    using Ricardo.CommonLibraries.Extensions.Tests.Mocks;
    using Ricardo.MVCPrueba1.Application.Converter.PersonsViewModel.ToPersonEntity.Properties;
    using Ricardo.MVCPrueba1.Application.Models;
    using Ricardo.MVCPrueba1.Application.Tests.Constants;
    using Ricardo.MVCPrueba1.Application.UserInfo;
    using Ricardo.MVCPrueba1.Domain.Entities;

    [TestCategory("Converter")]
    [TestClass]
    internal class UserIdConverterTests
    {
        private Mock<IPersonUserDetails> mockPersonUserDetails;

        [TestInitialize]
        public void Initilize()
        {
            this.mockPersonUserDetails = new Mock<IPersonUserDetails>(MockBehavior.Strict);
        }

        [TestCleanup]
        public void Verify()
        {
            this.mockPersonUserDetails.VerifyAllAndOtherCalls();
        }

        [TestMethod]
        public void Converter_WithValidData_ReturnValidResult()
        {
            // Arrange.
            this.mockPersonUserDetails
                .Setup(m => m.UserId)
                .Returns(PersonConstants.Id)
                .Verifiable(Times.Once);

            UserIdConverter propertyConverter = new UserIdConverter(this.mockPersonUserDetails.Object);

            PersonViewModel sourceClass = new PersonViewModel();

            PersonEntity result = new PersonEntity()
            {
                DNI = string.Empty,
                UserId = string.Empty,
            };

            // Act.
            propertyConverter.Convert(sourceClass, result);

            PersonEntity expectedResult = new PersonEntity()
            {
                DNI = string.Empty,
                UserId = PersonConstants.Id,
            };

            // Assert.
            result
                .Should()
                .BeEquivalentTo(expectedResult);
        }

        [TestMethod]
        public void Converter_WithEmptyData_ReturnEmptyResult()
        {
            // Arrange.
            this.mockPersonUserDetails
                .Setup(m => m.UserId)
                .Returns(PersonConstants.Id)
                .Verifiable(Times.Never);

            UserIdConverter propertyConverter = new UserIdConverter(this.mockPersonUserDetails.Object);

            PersonViewModel sourceClass = default;

            PersonEntity result = default;

            // Act.
            propertyConverter.Convert(sourceClass, result);

            PersonEntity expectedResult = default;

            // Assert.
            result
                .Should()
                .BeEquivalentTo(expectedResult);
        }
    }
}