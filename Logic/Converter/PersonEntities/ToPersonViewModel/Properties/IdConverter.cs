// <copyright file="IdConverter.cs" company="Ricardo">
// Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace MVCPrueba1.Logic.Converter.PersonEntities.ToPersonViewModel.Properties
{
    using MVCPrueba1.Entities;
    using MVCPrueba1.Models;
    using Ricardo.CommonLibraries.Converters;

    internal class IdConverter : ClassPropertyConverterBase<
        PersonEntity,
        PersonViewModel,
        Guid>,
        IPersonEntitiesToPersonViewModelPropertyConverter
    {
        protected override Guid GetPropertyValue(PersonEntity source)
        {
            return source.Id;
        }

        protected override void SetPropertyValue(in PersonViewModel result, Guid propertyValue)
        {
            result.Id = propertyValue;
        }
    }
}