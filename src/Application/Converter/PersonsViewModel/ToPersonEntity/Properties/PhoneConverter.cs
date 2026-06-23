// <copyright file="PhoneConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.Converter.PersonsViewModel.ToPersonEntity.Properties
{
    using Ricardo.Application.Models;
    using Ricardo.CommonLibraries.Converters;
    using Ricardo.Domain.Entities;

    internal class PhoneConverter : ClassPropertyConverterBase<
        PersonViewModel,
        PersonEntity,
        string>,
        IPersonsViewModelToPersonEntityPorpertyConverter
    {
        protected override string GetPropertyValue(PersonViewModel source)
        {
            return source.Phone;
        }

        protected override void SetPropertyValue(in PersonEntity result, string propertyValue)
        {
            result.Phone = propertyValue;
        }
    }
}