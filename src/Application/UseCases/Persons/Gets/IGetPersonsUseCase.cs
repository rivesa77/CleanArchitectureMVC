// <copyright file="IGetPersonsUseCase.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.UseCases.Persons.Gets
{
    using Ricardo.Application.Models;

    public interface IGetPersonsUseCase : IGetPersons<IEnumerable<PersonViewModel>>
    {
    }
}