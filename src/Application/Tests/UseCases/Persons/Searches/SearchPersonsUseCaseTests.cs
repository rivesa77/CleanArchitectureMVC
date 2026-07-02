// <copyright file="SearchPersonsUseCaseTests.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.CleanArchitectureMVC.Application.Tests.UseCases.Persons.Searches
{
    using FluentAssertions;
    using Moq;
    using Ricardo.CleanArchitectureMVC.Application.Converters.PersonEntities.ToPersonViewModel;
    using Ricardo.CleanArchitectureMVC.Application.Converters.PersonsSearchCriteria.ToPersonSearchQuery;
    using Ricardo.CleanArchitectureMVC.Application.Models;
    using Ricardo.CleanArchitectureMVC.Application.Repositories;
    using Ricardo.CleanArchitectureMVC.Application.Tests.Constants;
    using Ricardo.CleanArchitectureMVC.Application.UseCases.Persons.Searches;
    using Ricardo.CleanArchitectureMVC.Application.UserInfo;
    using Ricardo.CleanArchitectureMVC.Domain.Entities;
    using Ricardo.CommonLibraries.Extensions.Tests.Mocks;
    using ROP;

    [TestClass]
    [TestCategory("SearchPersonsUseCase")]
    public class SearchPersonsUseCaseTests
    {
        private Mock<IPersonRepository> mockPersonRepository;
        private Mock<IPersonUserDetails> mockPersonUserDetails;
        private Mock<IPersonEntitiesToPersonViewModelConverter> mockPersonViewModelConverter;
        private Mock<IPersonsSearchCriteriaToPersonSearchQueryConverter> mockPersonSearchQueryConverter;

        [TestInitialize]
        public void Initialize()
        {
            this.mockPersonRepository = new Mock<IPersonRepository>(MockBehavior.Strict);
            this.mockPersonUserDetails = new Mock<IPersonUserDetails>(MockBehavior.Strict);
            this.mockPersonViewModelConverter = new Mock<IPersonEntitiesToPersonViewModelConverter>(MockBehavior.Strict);
            this.mockPersonSearchQueryConverter = new Mock<IPersonsSearchCriteriaToPersonSearchQueryConverter>(MockBehavior.Strict);
        }

        [TestCleanup]
        public void Verify()
        {
            this.mockPersonRepository.VerifyAllAndOtherCalls();
            this.mockPersonUserDetails.VerifyAllAndOtherCalls();
            this.mockPersonViewModelConverter.VerifyAllAndOtherCalls();
            this.mockPersonSearchQueryConverter.VerifyAllAndOtherCalls();
        }

        [TestMethod]
        public async Task Execute_WhenRequestedPageExceedsTotalPages_ReturnsEffectiveLastPage()
        {
            // Arrange.
            PersonSearchCriteria criteria = new PersonSearchCriteria()
            {
                PageNumber = 10,
                PageSize = 5,
            };

            PersonSearchCriteria convertedCriteria = default;

            PersonSearchQuery personSearchQuery = new PersonSearchQuery()
            {
                PageNumber = criteria.PageNumber,
                PageSize = criteria.PageSize,
                UserId = "user-id",
            };

            PersonEntity personEntity = new PersonEntity()
            {
                DNI = PersonConstants.Dni,
                Email = PersonConstants.Email,
                Name = PersonConstants.Name,
                Phone = PersonConstants.Phone,
                UserId = personSearchQuery.UserId,
            };

            PersonViewModel personViewModel = new PersonViewModel()
            {
                DNI = PersonConstants.Dni,
                Email = PersonConstants.Email,
                Name = PersonConstants.Name,
                Phone = PersonConstants.Phone,
            };

            this.mockPersonSearchQueryConverter
                .Setup(m => m.Convert(It.IsAny<PersonSearchCriteria>()))
                .Callback<PersonSearchCriteria>(value => convertedCriteria = value)
                .Returns(personSearchQuery)
                .Verifiable(Times.Once());

            IEnumerable<PersonEntity> emptyPage = [];
            IEnumerable<PersonEntity> lastPage = [personEntity];

            this.mockPersonRepository
                .Setup(m => m.SearchByUserIdAsync(personSearchQuery))
                .ReturnsAsync(() => personSearchQuery.PageNumber == criteria.PageNumber
                    ? (emptyPage, 12)
                    : (lastPage, 12))
                .Verifiable(Times.Exactly(2));

            this.mockPersonViewModelConverter
                .Setup(m => m.Convert(personEntity))
                .Returns(personViewModel)
                .Verifiable(Times.Once());

            SearchPersonsUseCase useCase = this.CreateUseCase();

            // Act.
            Result<PersonSearchViewModel> result = await useCase.Execute(criteria).ConfigureAwait(false);

            // Assert.
            result.Errors.Should().BeEmpty();
            result.Value.PageNumber.Should().Be(3);
            result.Value.TotalPages.Should().Be(3);
            result.Value.Persons.Should().ContainSingle().Which.Should().BeSameAs(personViewModel);
            convertedCriteria.Should().NotBeSameAs(criteria);
            criteria.PageNumber.Should().Be(10);
        }

        [TestMethod]
        public async Task Execute_WhenCriteriaRequiresNormalization_DoesNotModifyInputCriteria()
        {
            // Arrange.
            PersonSearchCriteria criteria = new PersonSearchCriteria()
            {
                PageNumber = 0,
                PageSize = 99,
                SearchField = (PersonSearchField)999,
                SearchTerm = "  person  ",
                SortField = (PersonSortField)999,
                SortDirection = (PersonSortDirection)999,
            };

            PersonSearchCriteria convertedCriteria = default;

            PersonSearchQuery personSearchQuery = new PersonSearchQuery()
            {
                PageNumber = 1,
                PageSize = 5,
                UserId = "user-id",
            };

            this.mockPersonSearchQueryConverter
                .Setup(m => m.Convert(It.IsAny<PersonSearchCriteria>()))
                .Callback<PersonSearchCriteria>(value => convertedCriteria = value)
                .Returns(personSearchQuery)
                .Verifiable(Times.Once());

            this.mockPersonRepository
                .Setup(m => m.SearchByUserIdAsync(personSearchQuery))
                .ReturnsAsync((Enumerable.Empty<PersonEntity>(), 0))
                .Verifiable(Times.Once());

            SearchPersonsUseCase useCase = this.CreateUseCase();

            // Act.
            Result<PersonSearchViewModel> result = await useCase.Execute(criteria).ConfigureAwait(false);

            // Assert.
            result.Errors.Should().BeEmpty();
            result.Value.PageNumber.Should().Be(1);
            result.Value.PageSize.Should().Be(5);

            convertedCriteria.Should().NotBeSameAs(criteria);
            convertedCriteria.PageNumber.Should().Be(1);
            convertedCriteria.PageSize.Should().Be(5);
            convertedCriteria.SearchField.Should().Be(PersonSearchField.All);
            convertedCriteria.SearchTerm.Should().Be("person");
            convertedCriteria.SortField.Should().Be(PersonSortField.Name);
            convertedCriteria.SortDirection.Should().Be(PersonSortDirection.Ascending);

            criteria.PageNumber.Should().Be(0);
            criteria.PageSize.Should().Be(99);
            criteria.SearchField.Should().Be((PersonSearchField)999);
            criteria.SearchTerm.Should().Be("  person  ");
            criteria.SortField.Should().Be((PersonSortField)999);
            criteria.SortDirection.Should().Be((PersonSortDirection)999);
        }

        private SearchPersonsUseCase CreateUseCase()
        {
            return new SearchPersonsUseCase(
                this.mockPersonRepository.Object,
                this.mockPersonUserDetails.Object,
                this.mockPersonViewModelConverter.Object,
                this.mockPersonSearchQueryConverter.Object);
        }
    }
}