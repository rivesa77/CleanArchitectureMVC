// <copyright file="IPersonEntitiesToPersonViewModelConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.Converter.PersonEntities.ToPersonViewModel
{
    using Ricardo.Application.Models;
    using Ricardo.CommonLibraries.Converters;
    using Ricardo.Domain.Entities;

    internal interface IPersonEntitiesToPersonViewModelConverter : IClassConverter<PersonEntity, PersonViewModel>
    {
    }
}