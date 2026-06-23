// <copyright file="IGetPersonUseCase.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.UseCases.Persons.Gets
{
    using Ricardo.Application.Models;

    public interface IGetPersonUseCase : IPersonUseCase<Guid, PersonViewModel>
    {
    }
}