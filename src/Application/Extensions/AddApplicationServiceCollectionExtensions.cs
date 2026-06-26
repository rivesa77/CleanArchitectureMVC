// <copyright file="AddApplicationServiceCollectionExtensions.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Ricardo.MVCPrueba1.Application.Converter.Extensions;
    using Ricardo.MVCPrueba1.Application.UseCases.Extensions;

    public static class AddApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServiceCollection(this IServiceCollection services)
        {
            services
                .AddConvertersServiceCollection()
                .AddUseCaseServiceCollection();

            return services;
        }
    }
}