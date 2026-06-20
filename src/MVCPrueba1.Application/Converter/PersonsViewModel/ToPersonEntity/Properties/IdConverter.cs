// <copyright file="IdConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace MVCPrueba1.Application.Converter.PersonsViewModel.ToPersonEntity.Properties
{
    using System;
    using MVCPrueba1.Entities;
    using MVCPrueba1.Logic.Converter.PersonsViewModel.ToPersonEntity.Properties;
    using MVCPrueba1.Models;
    using Ricardo.CommonLibraries.Converters;

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