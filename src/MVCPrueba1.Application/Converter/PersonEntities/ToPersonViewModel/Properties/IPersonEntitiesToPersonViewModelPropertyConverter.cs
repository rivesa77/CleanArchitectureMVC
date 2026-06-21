// <copyright file="IPersonEntitiesToPersonViewModelPropertyConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace MVCPrueba1.Application.Converter.PersonEntities.ToPersonViewModel.Properties
{
    using MVCPrueba1.Application.Models;
    using MVCPrueba1.Domain.Entities;
    using Ricardo.CommonLibraries.Converters;

    internal interface IPersonEntitiesToPersonViewModelPropertyConverter :
        IClassPropertyConverter<PersonEntity, PersonViewModel>
    {
    }
}