// <copyright file="SearchPersonsUseCase.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.UseCases.Persons.Searches
{
    using Ricardo.MVCPrueba1.Application.Converter.PersonEntities.ToPersonViewModel;
    using Ricardo.MVCPrueba1.Application.Converter.PersonsSearchCriteria.ToPersonSearchQuery;
    using Ricardo.MVCPrueba1.Application.Models;
    using Ricardo.MVCPrueba1.Application.Repositories;
    using Ricardo.MVCPrueba1.Application.UserInfo;
    using Ricardo.MVCPrueba1.Domain.Entities;
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
            PersonSearchCriteria normalizedCriteria = criteria ?? new PersonSearchCriteria();

            normalizedCriteria.PageNumber = normalizedCriteria.PageNumber < 1 ? 1 : normalizedCriteria.PageNumber;

            normalizedCriteria.PageSize = AllowedPageSizes.Contains(normalizedCriteria.PageSize)
                ? normalizedCriteria.PageSize
                : AllowedPageSizes[0];

            normalizedCriteria.SearchField = Enum.IsDefined(normalizedCriteria.SearchField)
                ? normalizedCriteria.SearchField
                : PersonSearchField.All;

            normalizedCriteria.SearchTerm = normalizedCriteria.SearchTerm?.Trim();

            normalizedCriteria.SortField = Enum.IsDefined(normalizedCriteria.SortField)
                ? normalizedCriteria.SortField
                : PersonSortField.Name;

            normalizedCriteria.SortDirection = Enum.IsDefined(normalizedCriteria.SortDirection)
                ? normalizedCriteria.SortDirection
                : PersonSortDirection.Ascending;

            return normalizedCriteria;
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
            string userId = this.PersonUserDetails.UserId;

            PersonSearchQuery personSearchQuery = this.personSearchQueryConverter.Convert(criteria);

            (IEnumerable<PersonEntity> personEntities, int totalItems) = await this.PersonRepository
                .SearchByUserIdAsync(personSearchQuery)
                .ConfigureAwait(false);

            int totalPages = GetTotalPages(totalItems, criteria.PageSize);

            if (totalPages > 0 && criteria.PageNumber > totalPages)
            {
                personSearchQuery.PageNumber = totalPages;

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
                PageNumber = criteria.PageNumber,
                PageSize = criteria.PageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
            };
        }
    }
}
