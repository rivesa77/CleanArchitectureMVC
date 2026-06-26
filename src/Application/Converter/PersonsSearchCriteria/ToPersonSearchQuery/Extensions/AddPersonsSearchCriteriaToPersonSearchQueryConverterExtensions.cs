// <copyright file="AddPersonsSearchCriteriaToPersonSearchQueryConverterExtensions.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.Converter.PersonsSearchCriteria.ToPersonSearchQuery.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Ricardo.MVCPrueba1.Application.Converter.PersonsSearchCriteria.ToPersonSearchQuery.Properties;

    internal static class AddPersonsSearchCriteriaToPersonSearchQueryConverterExtensions
    {
        internal static IServiceCollection AddPersonsSearchCriteriaToPersonSearchQueryConverter(this IServiceCollection services)
        {
            services
                .AddScoped<
                    IPersonsSearchCriteriaToPersonSearchQueryConverter,
                    PersonsSearchCriteriaToPersonSearchQueryConverter>();

            services
                .AddScoped<IPersonsSearchCriteriaToPersonSearchQueryPropertyConverter, PageNumberConverter>()
                .AddScoped<IPersonsSearchCriteriaToPersonSearchQueryPropertyConverter, PageSizeConverter>()
                .AddScoped<IPersonsSearchCriteriaToPersonSearchQueryPropertyConverter, SearchFieldConverter>()
                .AddScoped<IPersonsSearchCriteriaToPersonSearchQueryPropertyConverter, SearchTermConverter>()
                .AddScoped<IPersonsSearchCriteriaToPersonSearchQueryPropertyConverter, SortDirectionConverter>()
                .AddScoped<IPersonsSearchCriteriaToPersonSearchQueryPropertyConverter, SortFieldConverter>()
                .AddScoped<IPersonsSearchCriteriaToPersonSearchQueryPropertyConverter, UserIdConverter>();

            return services;
        }
    }
}
