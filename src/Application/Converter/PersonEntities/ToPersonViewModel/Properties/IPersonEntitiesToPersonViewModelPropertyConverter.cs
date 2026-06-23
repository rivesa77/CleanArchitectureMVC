// <copyright file="IPersonEntitiesToPersonViewModelPropertyConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.Converter.PersonEntities.ToPersonViewModel.Properties
{
    using Ricardo.CommonLibraries.Converters;
    using Ricardo.MVCPrueba1.Application.Models;
    using Ricardo.MVCPrueba1.Domain.Entities;

    internal interface IPersonEntitiesToPersonViewModelPropertyConverter :
        IClassPropertyConverter<PersonEntity, PersonViewModel>
    {
    }
}