// <copyright file="IGetPersonsUseCase.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.UseCases.Persons.Gets
{
    using Ricardo.MVCPrueba1.Application.Models;

    public interface IGetPersonsUseCase : IGetPersons<IEnumerable<PersonViewModel>>
    {
    }
}