// <copyright file="IPersonsSearchCriteriaToPersonSearchQueryPropertyConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.Converter.PersonsSearchCriteria.ToPersonSearchQuery.Properties
{
    using Ricardo.CommonLibraries.Converters;
    using Ricardo.MVCPrueba1.Application.Repositories;
    using Ricardo.MVCPrueba1.Application.UseCases.Persons.Searches;

    /// <inheritdoc/>
    internal interface IPersonsSearchCriteriaToPersonSearchQueryPropertyConverter :
        IClassPropertyConverter<PersonSearchCriteria, PersonSearchQuery>
    {
    }
}