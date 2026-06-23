// <copyright file="IPersonsViewModelToPersonEntityPorpertyConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.Converter.PersonsViewModel.ToPersonEntity.Properties
{
    using Ricardo.Application.Models;
    using Ricardo.CommonLibraries.Converters;
    using Ricardo.Domain.Entities;

    internal interface IPersonsViewModelToPersonEntityPorpertyConverter :
        IClassPropertyConverter<PersonViewModel, PersonEntity>
    {
    }
}