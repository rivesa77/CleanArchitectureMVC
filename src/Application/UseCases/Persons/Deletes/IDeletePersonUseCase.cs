// <copyright file="IDeletePersonUseCase.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.UseCases.Persons.Deletes
{
    using Ricardo.Application.Models;

    public interface IDeletePersonUseCase : IPersonUseCase<PersonViewModel, bool>
    {
    }
}