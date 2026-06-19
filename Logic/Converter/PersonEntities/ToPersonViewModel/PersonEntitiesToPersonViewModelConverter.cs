// <copyright file="PersonEntitiesToPersonViewModelConverter.cs" company="Ricardo">
// Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace MVCPrueba1.Logic.Converter.PersonEntities.ToPersonViewModel
{
    using MVCPrueba1.Entities;
    using MVCPrueba1.Logic.Converter.PersonEntities.ToPersonViewModel.Properties;
    using MVCPrueba1.Models;
    using Ricardo.CommonLibraries.Converters;

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