// <copyright file="PhoneConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.Converter.PersonEntities.ToPersonViewModel.Properties
{
    using Ricardo.Application.Models;
    using Ricardo.CommonLibraries.Converters;
    using Ricardo.Domain.Entities;

    internal class PhoneConverter : ClassPropertyConverterBase<
        PersonEntity,
        PersonViewModel,
        string>,
        IPersonEntitiesToPersonViewModelPropertyConverter
    {
        protected override string GetPropertyValue(PersonEntity source)
        {
            return source.Phone;
        }

        protected override void SetPropertyValue(in PersonViewModel result, string propertyValue)
        {
            result.Phone = propertyValue;
        }
    }
}