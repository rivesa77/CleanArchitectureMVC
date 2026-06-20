// <copyright file="AddPersonViewModelToPersonEntitiesConverterExtension.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace MVCPrueba1.Logic.Converter.PersonsViewModel.ToPersonEntity.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using MVCPrueba1.Application.Converter.PersonsViewModel.ToPersonEntity.Properties;
    using MVCPrueba1.Logic.Converter.PersonsViewModel.ToPersonEntity.Properties;

    internal static class AddPersonViewModelToPersonEntitiesConverterExtension
    {
        internal static IServiceCollection AddPersonViewModelToPersonEntitiesConverter(this IServiceCollection services)
        {
            services
                .AddScoped<
                    IPersonsViewModelToPersonEntityConverter,
                    PersonsViewModelToPersonEntityConverter>();

            services
                .AddScoped<IPersonsViewModelToPersonEntityPorpertyConverter, DniConverter>()
                .AddScoped<IPersonsViewModelToPersonEntityPorpertyConverter, EmailConverter>()
                .AddScoped<IPersonsViewModelToPersonEntityPorpertyConverter, IdConverter>()
                .AddScoped<IPersonsViewModelToPersonEntityPorpertyConverter, NameConverter>()
                .AddScoped<IPersonsViewModelToPersonEntityPorpertyConverter, PhoneConverter>()
                .AddScoped<IPersonsViewModelToPersonEntityPorpertyConverter, UserIdConverter>();

            return services;
        }
    }
}