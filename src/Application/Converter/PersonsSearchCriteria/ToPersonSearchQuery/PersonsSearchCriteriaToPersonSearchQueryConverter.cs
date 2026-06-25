// <copyright file="PersonsSearchCriteriaToPersonSearchQueryConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.Converter.PersonsSearchCriteria.ToPersonSearchQuery
{
    using System.Collections.Generic;
    using Ricardo.Application.Converter.PersonsSearchCriteria.ToPersonSearchQuery.Properties;
    using Ricardo.CommonLibraries.Converters;
    using Ricardo.MVCPrueba1.Application.Repositories;
    using Ricardo.MVCPrueba1.Application.UseCases.Persons.Searches;

    /// <inheritdoc/>
    internal class PersonsSearchCriteriaToPersonSearchQueryConverter : ClassConverterBase<
        PersonSearchCriteria,
        PersonSearchQuery,
        IPersonsSearchCriteriaToPersonSearchQueryPropertyConverter>,
        IPersonsSearchCriteriaToPersonSearchQueryConverter
    {
        public PersonsSearchCriteriaToPersonSearchQueryConverter(
            IEnumerable<IPersonsSearchCriteriaToPersonSearchQueryPropertyConverter> propertyConverters)
            : base(propertyConverters)
        {
        }
    }
}