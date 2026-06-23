// <copyright file="IGetPersons.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.UseCases.Persons.Gets
{
    using ROP;

    public interface IGetPersons<TDestinationClass>
        where TDestinationClass : class
    {
        Task<Result<TDestinationClass>> Execute();
    }
}