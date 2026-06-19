// <copyright file="IPersonEntitiesToPersonViewModelConverter.cs" company="Ricardo">
// Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace MVCPrueba1.Logic.Converter.PersonEntities.ToPersonViewModel
{
    using MVCPrueba1.Entities;
    using MVCPrueba1.Models;
    using Ricardo.CommonLibraries.Converters;

    internal interface IPersonEntitiesToPersonViewModelConverter : IClassConverter<PersonEntity, PersonViewModel>
    {
    }
}