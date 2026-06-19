// <copyright file="PhoneConverter.cs" company="Ricardo">
// Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace MVCPrueba1.Logic.Converter.PersonEntities.ToPersonViewModel.Properties
{
    using MVCPrueba1.Entities;
    using MVCPrueba1.Models;
    using Ricardo.CommonLibraries.Converters;

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