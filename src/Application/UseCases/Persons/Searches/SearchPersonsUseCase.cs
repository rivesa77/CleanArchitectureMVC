// <copyright file="SearchPersonsUseCase.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.CleanArchitectureMVC.Application.UseCases.Persons.Searches
{
    using Ricardo.CleanArchitectureMVC.Application.Converters.PersonEntities.ToPersonViewModel;
    using Ricardo.CleanArchitectureMVC.Application.Converters.PersonsSearchCriteria.ToPersonSearchQuery;
    using Ricardo.CleanArchitectureMVC.Application.Models;
    using Ricardo.CleanArchitectureMVC.Application.Repositories;
    using Ricardo.CleanArchitectureMVC.Application.UserInfo;
    using Ricardo.CleanArchitectureMVC.Domain.Entities;
    using ROP;

    internal class SearchPersonsUseCase : PersonUseCaseBase, ISearchPersonsUseCase
    {
        private static readonly int[] AllowedPageSizes = [5, 10, 15];
        private readonly IPersonEntitiesToPersonViewModelConverter pesonViewModelConverter;
        private readonly IPersonsSearchCriteriaToPersonSearchQueryConverter personSearchQueryConverter;

        public SearchPersonsUseCase(
            IPersonRepository personRepository,
            IPersonUserDetails personUserDetails,
            IPersonEntitiesToPersonViewModelConverter converter,
            IPersonsSearchCriteriaToPersonSearchQueryConverter personSearchQueryConverter)
                : base(personRepository, personUserDetails)
        {
            this.pesonViewModelConverter = converter;
            this.personSearchQueryConverter = personSearchQueryConverter;
        }

        public async Task<Result<PersonSearchViewModel>> Execute(PersonSearchCriteria criteria)
        {
            PersonSearchCriteria normalizedCriteria = NormalizeCriteria(criteria);

            PersonSearchViewModel persons = await this.SearchPersons(normalizedCriteria)
                .ConfigureAwait(false);

            return Result.Success(persons);
        }

        private static PersonSearchCriteria NormalizeCriteria(PersonSearchCriteria criteria)
        {
            criteria ??= new PersonSearchCriteria();

            return new PersonSearchCriteria()
            {
                PageNumber = criteria.PageNumber < 1 ? 1 : criteria.PageNumber,
                PageSize = AllowedPageSizes.Contains(criteria.PageSize)
                    ? criteria.PageSize
                    : AllowedPageSizes[0],
                SearchField = Enum.IsDefined(criteria.SearchField)
                    ? criteria.SearchField
                    : PersonSearchField.All,
                SearchTerm = criteria.SearchTerm?.Trim(),
                SortField = Enum.IsDefined(criteria.SortField)
                    ? criteria.SortField
                    : PersonSortField.Name,
                SortDirection = Enum.IsDefined(criteria.SortDirection)
                    ? criteria.SortDirection
                    : PersonSortDirection.Ascending,
            };
        }

        private static int GetTotalPages(int totalItems, int pageSize)
        {
            if (totalItems <= 0)
            {
                return 0;
            }

            return (int)Math.Ceiling(totalItems / (double)pageSize);
        }

        private async Task<PersonSearchViewModel> SearchPersons(PersonSearchCriteria criteria)
        {
            PersonSearchQuery personSearchQuery = this.personSearchQueryConverter.Convert(criteria);

            (IEnumerable<PersonEntity> personEntities, int totalItems) = await this.PersonRepository
                .SearchByUserIdAsync(personSearchQuery)
                .ConfigureAwait(false);

            int totalPages = GetTotalPages(totalItems, criteria.PageSize);

            int effectivePageNumber = totalPages == 0
                ? 1
                : Math.Min(criteria.PageNumber, totalPages);

            if (effectivePageNumber != criteria.PageNumber && totalPages > 0)
            {
                personSearchQuery.PageNumber = effectivePageNumber;

                (personEntities, totalItems) = await this.PersonRepository
                    .SearchByUserIdAsync(personSearchQuery)
                    .ConfigureAwait(false);

                totalPages = GetTotalPages(totalItems, criteria.PageSize);
            }

            IEnumerable<PersonViewModel> personViewModels = personEntities.Select(p => this.pesonViewModelConverter.Convert(p));

            return new PersonSearchViewModel()
            {
                Persons = personViewModels,
                SearchField = criteria.SearchField,
                SearchTerm = criteria.SearchTerm,
                SortField = criteria.SortField,
                SortDirection = criteria.SortDirection,
                PageNumber = effectivePageNumber,
                PageSize = criteria.PageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
            };
        }
    }
}