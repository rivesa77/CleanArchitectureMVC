// <copyright file="IUpdatePersonUseCase.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.UseCases.Persons.Updates
{
    using Ricardo.MVCPrueba1.Application.Models;

    public interface IUpdatePersonUseCase : IPersonUseCase<PersonViewModel, bool>
    {
    }
}