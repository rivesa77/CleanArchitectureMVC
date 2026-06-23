// <copyright file="IPersonsViewModelToPersonEntityConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.Converter.PersonsViewModel.ToPersonEntity
{
    using Ricardo.Application.Models;
    using Ricardo.CommonLibraries.Converters;
    using Ricardo.Domain.Entities;

    internal interface IPersonsViewModelToPersonEntityConverter : IClassConverter<PersonViewModel, PersonEntity>
    {
    }
}