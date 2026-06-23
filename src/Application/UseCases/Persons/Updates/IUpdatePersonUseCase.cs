// <copyright file="IUpdatePersonUseCase.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.UseCases.Persons.Updates
{
    using Ricardo.Application.Models;

    public interface IUpdatePersonUseCase : IPersonUseCase<PersonViewModel, bool>
    {
    }
}