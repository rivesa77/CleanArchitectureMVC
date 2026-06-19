// <copyright file="AddConvertersExtension.cs" company="Ricardo">
// Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace MVCPrueba1.Logic.Converter.Extensions
{
    using MVCPrueba1.Logic.Converter.PersonEntities.ToPersonViewModel.Extensions;

    internal static class AddConvertersExtension
    {
        internal static IServiceCollection AddConverters(this IServiceCollection services)
        {
            services.AddPersonEntitiesToPersonViewModelConverter();

            return services;
        }
    }
}