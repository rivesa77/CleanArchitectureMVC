// <copyright file="NameConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace MVCPrueba1.Application.Converter.PersonEntities.ToPersonViewModel.Properties
{
    using MVCPrueba1.Application.Models;
    using MVCPrueba1.Domain.Entities;
    using Ricardo.CommonLibraries.Converters;

    internal class NameConverter : ClassPropertyConverterBase<
        PersonEntity,
        PersonViewModel,
        string>,
        IPersonEntitiesToPersonViewModelPropertyConverter
    {
        protected override string GetPropertyValue(PersonEntity source)
        {
            return source.Name;
        }

        protected override void SetPropertyValue(in PersonViewModel result, string propertyValue)
        {
            result.Name = propertyValue;
        }
    }
}