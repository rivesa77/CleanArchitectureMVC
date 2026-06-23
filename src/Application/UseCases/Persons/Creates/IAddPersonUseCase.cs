// <copyright file="IAddPersonUseCase.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.UseCases.Persons.Creates
{
    using Ricardo.Application.Models;

    public interface IAddPersonUseCase : IPersonUseCase<PersonViewModel, bool>
    {
    }
}