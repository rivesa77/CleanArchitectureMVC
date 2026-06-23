// <copyright file="AddPersonEntitiesToPersonViewModelConverterExtension.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.Converter.PersonEntities.ToPersonViewModel.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Ricardo.Application.Converter.PersonEntities.ToPersonViewModel.Properties;

    internal static class AddPersonEntitiesToPersonViewModelConverterExtension
    {
        internal static IServiceCollection AddPersonEntitiesToPersonViewModelConverter(this IServiceCollection services)
        {
            services
                .AddScoped<
                    IPersonEntitiesToPersonViewModelConverter,
                    PersonEntitiesToPersonViewModelConverter>();

            services
                .AddScoped<IPersonEntitiesToPersonViewModelPropertyConverter, DniConverter>()
                .AddScoped<IPersonEntitiesToPersonViewModelPropertyConverter, EmailConverter>()
                .AddScoped<IPersonEntitiesToPersonViewModelPropertyConverter, IdConverter>()
                .AddScoped<IPersonEntitiesToPersonViewModelPropertyConverter, NameConverter>()
                .AddScoped<IPersonEntitiesToPersonViewModelPropertyConverter, PhoneConverter>();

            return services;
        }
    }
}
