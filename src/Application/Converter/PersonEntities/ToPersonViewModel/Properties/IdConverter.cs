// <copyright file="IdConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.Converter.PersonEntities.ToPersonViewModel.Properties
{
    using Ricardo.Application.Models;
    using Ricardo.CommonLibraries.Converters;
    using Ricardo.Domain.Entities;

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