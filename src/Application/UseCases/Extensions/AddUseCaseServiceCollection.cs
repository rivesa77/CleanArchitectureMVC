// <copyright file="AddUseCaseServiceCollection.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.UseCases.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Ricardo.MVCPrueba1.Application.UseCases.Persons.Creates;
    using Ricardo.MVCPrueba1.Application.UseCases.Persons.Deletes;
    using Ricardo.MVCPrueba1.Application.UseCases.Persons.Gets;
    using Ricardo.MVCPrueba1.Application.UseCases.Persons.Updates;

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