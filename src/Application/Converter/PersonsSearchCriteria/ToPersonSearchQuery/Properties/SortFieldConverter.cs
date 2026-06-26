// <copyright file="SortFieldConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.Converter.PersonsSearchCriteria.ToPersonSearchQuery.Properties
{
    using Ricardo.CommonLibraries.Converters;
    using Ricardo.MVCPrueba1.Application.Models;
    using Ricardo.MVCPrueba1.Application.Repositories;
    using Ricardo.MVCPrueba1.Application.UseCases.Persons.Searches;

    /// <inheritdoc/>
    internal class SortFieldConverter : ClassPropertyConverterBase<
        PersonSearchCriteria,
        PersonSearchQuery,
        PersonSortField>,
        IPersonsSearchCriteriaToPersonSearchQueryPropertyConverter
    {
        /// <inheritdoc/>
        protected override PersonSortField GetPropertyValue(PersonSearchCriteria source)
        {
            return source.SortField;
        }

        /// <inheritdoc/>
        protected override void SetPropertyValue(in PersonSearchQuery result, PersonSortField propertyValue)
        {
            result.SortField = propertyValue;
        }
    }
}
