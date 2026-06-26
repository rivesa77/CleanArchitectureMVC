// <copyright file="AddConvertersServiceCollectionExtension.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.Converter.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Ricardo.Application.Converter.PersonsSearchCriteria.ToPersonSearchQuery.Extensions;
    using Ricardo.MVCPrueba1.Application.Converter.PersonEntities.ToPersonViewModel.Extensions;
    using Ricardo.MVCPrueba1.Application.Converter.PersonsViewModel.ToPersonEntity.Extensions;

    internal static class AddConvertersServiceCollectionExtension
    {
        internal static IServiceCollection AddConvertersServiceCollection(this IServiceCollection services)
        {
            services
                .AddPersonEntitiesToPersonViewModelConverter()
                .AddPersonsSearchCriteriaToPersonSearchQueryConverter()
                .AddPersonViewModelToPersonEntitiesConverter();

            return services;
        }
    }
}