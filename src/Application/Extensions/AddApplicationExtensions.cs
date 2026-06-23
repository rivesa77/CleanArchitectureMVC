// <copyright file="AddApplicationExtensions.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Ricardo.Application.Converter.Extensions;
    using Ricardo.Application.UseCases.Extensions;

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