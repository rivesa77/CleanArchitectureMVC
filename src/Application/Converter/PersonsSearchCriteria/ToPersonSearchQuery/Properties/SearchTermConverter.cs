// <copyright file="SearchTermConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.Converter.PersonsSearchCriteria.ToPersonSearchQuery.Properties
{
    using Ricardo.CommonLibraries.Converters;
    using Ricardo.MVCPrueba1.Application.Repositories;
    using Ricardo.MVCPrueba1.Application.UseCases.Persons.Searches;

    /// <inheritdoc/>
    internal class SearchTermConverter : ClassPropertyConverterBase<
        PersonSearchCriteria,
        PersonSearchQuery,
        string>,
        IPersonsSearchCriteriaToPersonSearchQueryPropertyConverter
    {
        /// <inheritdoc/>
        protected override string GetPropertyValue(PersonSearchCriteria source)
        {
            return source.SearchTerm;
        }

        /// <inheritdoc/>
        protected override void SetPropertyValue(in PersonSearchQuery result, string propertyValue)
        {
            result.SearchTerm = propertyValue;
        }
    }
}