// <copyright file="AddConvertersExtension.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.Converter.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Ricardo.Application.Converter.PersonEntities.ToPersonViewModel.Extensions;
    using Ricardo.Application.Converter.PersonsViewModel.ToPersonEntity.Extensions;

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
