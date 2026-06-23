// <copyright file="IPersonUseCase.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.UseCases.Persons
{
    using ROP;

    public interface IPersonUseCase<TSourceClass, TDestinationClass>
    {
        Task<Result<TDestinationClass>> Execute(TSourceClass sourceClass);
    }
}