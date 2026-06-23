// <copyright file="IDeletePersonUseCase.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.UseCases.Persons.Deletes
{
    using Ricardo.MVCPrueba1.Application.Models;

    public interface IDeletePersonUseCase : IPersonUseCase<PersonViewModel, bool>
    {
    }
}