// <copyright file="IDeletePersonUseCase.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace MVCPrueba1.Application.UseCases.Persons
{
    using MVCPrueba1.Logic.UseCases.Persons;
    using MVCPrueba1.Models;

    public interface IDeletePersonUseCase : IPersonUseCase<PersonViewModel, bool>
    {
    }
}