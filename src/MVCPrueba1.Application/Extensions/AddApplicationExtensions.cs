// <copyright file="AddApplicationExtensions.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace MVCPrueba1.Logic.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using MVCPrueba1.Logic.Converter.Extensions;
    using MVCPrueba1.Logic.UseCases.Extensions;

    public static class AddApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddConverters()
                .AddUseCases();

            return services;
        }
    }
}