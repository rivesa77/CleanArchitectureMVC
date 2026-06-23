// <copyright file="PersonEntitiesToPersonViewModelConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.Converter.PersonEntities.ToPersonViewModel
{
    using Ricardo.Application.Converter.PersonEntities.ToPersonViewModel.Properties;
    using Ricardo.Application.Models;
    using Ricardo.CommonLibraries.Converters;
    using Ricardo.Domain.Entities;

    internal class PersonEntitiesToPersonViewModelConverter : ClassConverterBase<
        PersonEntity,
        PersonViewModel,
        IPersonEntitiesToPersonViewModelPropertyConverter>,
        IPersonEntitiesToPersonViewModelConverter
    {
        public PersonEntitiesToPersonViewModelConverter(
            IEnumerable<IPersonEntitiesToPersonViewModelPropertyConverter> propertyConverters)
            : base(propertyConverters)
        {
        }
    }
}