// <copyright file="IPersonEntitiesToPersonViewModelPropertyConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.Converter.PersonEntities.ToPersonViewModel.Properties
{
    using Ricardo.Application.Models;
    using Ricardo.CommonLibraries.Converters;
    using Ricardo.Domain.Entities;

    internal interface IPersonEntitiesToPersonViewModelPropertyConverter :
        IClassPropertyConverter<PersonEntity, PersonViewModel>
    {
    }
}