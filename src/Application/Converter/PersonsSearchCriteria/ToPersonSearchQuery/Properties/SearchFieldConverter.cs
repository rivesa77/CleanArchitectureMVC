// <copyright file="SearchFieldConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.Converter.PersonsSearchCriteria.ToPersonSearchQuery.Properties
{
    using Ricardo.CommonLibraries.Converters;
    using Ricardo.MVCPrueba1.Application.Models;
    using Ricardo.MVCPrueba1.Application.Repositories;
    using Ricardo.MVCPrueba1.Application.UseCases.Persons.Searches;

    /// <inheritdoc/>
    internal class SearchFieldConverter : ClassPropertyConverterBase<
        PersonSearchCriteria,
        PersonSearchQuery,
        PersonSearchField>,
        IPersonsSearchCriteriaToPersonSearchQueryPropertyConverter
    {
        /// <inheritdoc/>
        protected override PersonSearchField GetPropertyValue(PersonSearchCriteria source)
        {
            return source.SearchField;
        }

        /// <inheritdoc/>
        protected override void SetPropertyValue(in PersonSearchQuery result, PersonSearchField propertyValue)
        {
            result.SearchField = propertyValue;
        }
    }
}