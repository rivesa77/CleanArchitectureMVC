// <copyright file="IAddPersonUseCase.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.UseCases.Persons.Creates
{
    using Ricardo.MVCPrueba1.Application.Models;

    public interface IAddPersonUseCase : IPersonUseCase<PersonViewModel, bool>
    {
    }
}