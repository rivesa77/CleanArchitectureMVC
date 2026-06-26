// <copyright file="IPersonsSearchCriteriaToPersonSearchQueryConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.Converter.PersonsSearchCriteria.ToPersonSearchQuery
{
    using Ricardo.CommonLibraries.Converters;
    using Ricardo.MVCPrueba1.Application.Repositories;
    using Ricardo.MVCPrueba1.Application.UseCases.Persons.Searches;

    /// <inheritdoc/>
    internal interface IPersonsSearchCriteriaToPersonSearchQueryConverter : IClassConverter<PersonSearchCriteria, PersonSearchQuery>
    {
    }
}