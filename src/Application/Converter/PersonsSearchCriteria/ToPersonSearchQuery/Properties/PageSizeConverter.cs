// <copyright file="PageSizeConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.Converter.PersonsSearchCriteria.ToPersonSearchQuery.Properties
{
    using Ricardo.CommonLibraries.Converters;
    using Ricardo.MVCPrueba1.Application.Repositories;
    using Ricardo.MVCPrueba1.Application.UseCases.Persons.Searches;

    /// <inheritdoc/>
    internal class PageSizeConverter : ClassPropertyConverterBase<
        PersonSearchCriteria,
        PersonSearchQuery,
        int>,
        IPersonsSearchCriteriaToPersonSearchQueryPropertyConverter
    {
        /// <inheritdoc/>
        protected override int GetPropertyValue(PersonSearchCriteria source)
        {
            return source.PageSize;
        }

        /// <inheritdoc/>
        protected override void SetPropertyValue(in PersonSearchQuery result, int propertyValue)
        {
            result.PageSize = propertyValue;
        }
    }
}