// <copyright file="PersonEntitiesToPersonViewModelConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.Converter.PersonEntities.ToPersonViewModel
{
    using Ricardo.CommonLibraries.Converters;
    using Ricardo.MVCPrueba1.Application.Converter.PersonEntities.ToPersonViewModel.Properties;
    using Ricardo.MVCPrueba1.Application.Models;
    using Ricardo.MVCPrueba1.Domain.Entities;

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