// <copyright file="ISearchPersonsUseCase.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.UseCases.Persons.Searches
{
    using Ricardo.MVCPrueba1.Application.Models;

    public interface ISearchPersonsUseCase : IPersonUseCase<PersonSearchCriteria, PersonSearchViewModel>
    {
    }
}
