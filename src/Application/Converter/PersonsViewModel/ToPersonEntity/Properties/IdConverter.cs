// <copyright file="IdConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.Converter.PersonsViewModel.ToPersonEntity.Properties
{
    using System;
    using Ricardo.Application.Models;
    using Ricardo.CommonLibraries.Converters;
    using Ricardo.Domain.Entities;

    internal class IdConverter : ClassPropertyConverterBase<
        PersonViewModel,
        PersonEntity,
        Guid>,
        IPersonsViewModelToPersonEntityPorpertyConverter
    {
        protected override Guid GetPropertyValue(PersonViewModel source)
        {
            return source.Id;
        }

        protected override void SetPropertyValue(in PersonEntity result, Guid propertyValue)
        {
            result.Id = propertyValue;
        }
    }
}