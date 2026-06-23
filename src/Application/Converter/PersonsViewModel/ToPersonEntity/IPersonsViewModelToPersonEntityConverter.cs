// <copyright file="IPersonsViewModelToPersonEntityConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.Converter.PersonsViewModel.ToPersonEntity
{
    using Ricardo.CommonLibraries.Converters;
    using Ricardo.MVCPrueba1.Application.Models;
    using Ricardo.MVCPrueba1.Domain.Entities;

    internal interface IPersonsViewModelToPersonEntityConverter : IClassConverter<PersonViewModel, PersonEntity>
    {
    }
}