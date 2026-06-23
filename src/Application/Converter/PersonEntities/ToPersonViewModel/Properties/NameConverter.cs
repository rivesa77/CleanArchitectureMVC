// <copyright file="NameConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.Converter.PersonEntities.ToPersonViewModel.Properties
{
    using Ricardo.CommonLibraries.Converters;
    using Ricardo.MVCPrueba1.Application.Models;
    using Ricardo.MVCPrueba1.Domain.Entities;

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