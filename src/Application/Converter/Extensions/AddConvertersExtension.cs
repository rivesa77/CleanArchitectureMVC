// <copyright file="AddConvertersExtension.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.Converter.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Ricardo.MVCPrueba1.Application.Converter.PersonEntities.ToPersonViewModel.Extensions;
    using Ricardo.MVCPrueba1.Application.Converter.PersonsViewModel.ToPersonEntity.Extensions;

    internal static class AddConvertersExtension
    {
        internal static IServiceCollection AddConverters(this IServiceCollection services)
        {
            services
                .AddPersonEntitiesToPersonViewModelConverter()
                .AddPersonViewModelToPersonEntitiesConverter();

            return services;
        }
    }
}
