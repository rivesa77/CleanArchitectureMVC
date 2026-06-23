// <copyright file="IPersonEntitiesToPersonViewModelConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.Converter.PersonEntities.ToPersonViewModel
{
    using Ricardo.CommonLibraries.Converters;
    using Ricardo.MVCPrueba1.Application.Models;
    using Ricardo.MVCPrueba1.Domain.Entities;

    internal interface IPersonEntitiesToPersonViewModelConverter : IClassConverter<PersonEntity, PersonViewModel>
    {
    }
}