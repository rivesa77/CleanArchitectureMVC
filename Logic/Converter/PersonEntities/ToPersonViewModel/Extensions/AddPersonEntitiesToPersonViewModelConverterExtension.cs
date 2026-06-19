// <copyright file="AddPersonEntitiesToPersonViewModelConverterExtension.cs" company="Ricardo">
// Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace MVCPrueba1.Logic.Converter.PersonEntities.ToPersonViewModel.Extensions
{
    using MVCPrueba1.Logic.Converter.PersonEntities.ToPersonViewModel.Properties;

    internal static class AddPersonEntitiesToPersonViewModelConverterExtension
    {
        internal static IServiceCollection AddPersonEntitiesToPersonViewModelConverter(this IServiceCollection services)
        {
            services
                .AddScoped<
                    IPersonEntitiesToPersonViewModelConverter,
                    PersonEntitiesToPersonViewModelConverter>();

            services
                .AddScoped<
                    IPersonEntitiesToPersonViewModelPropertyConverter,
                    NameConverter>();

            return services;
        }
    }
}