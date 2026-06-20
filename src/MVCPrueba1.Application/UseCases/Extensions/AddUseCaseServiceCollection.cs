// <copyright file="AddUseCaseServiceCollection.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace MVCPrueba1.Logic.UseCases.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using MVCPrueba1.Application.UseCases.Persons;
    using MVCPrueba1.Logic.UseCases.Persons;

    public static class AddUseCaseServiceCollection
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services
                .AddScoped<IAddPersonUseCase, AddPersonUseCase>()
                .AddScoped<IDeletePersonUseCase, DeletePersonUseCase>()
                .AddScoped<IGetPersonUseCase, GetPersonUseCase>()
                .AddScoped<IGetPersonsUseCase, GetPersonsUseCase>()
                .AddScoped<IUpdatePersonUseCase, UpdatePersonUseCase>();

            return services;
        }
    }
}