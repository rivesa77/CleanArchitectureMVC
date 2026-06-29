// <copyright file="PersonsViewModelToPersonEntityConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.Converter.PersonsViewModel.ToPersonEntity
{
    using Ricardo.CommonLibraries.Converters;
    using Ricardo.MVCPrueba1.Application.Converter.PersonsViewModel.ToPersonEntity.Properties;
    using Ricardo.MVCPrueba1.Application.Models;
    using Ricardo.MVCPrueba1.Domain.Entities;

    internal class PersonsViewModelToPersonEntityConverter : ClassConverterNonInitializeBase<
        PersonViewModel,
        PersonEntity,
        IPersonsViewModelToPersonEntityPropertyConverter>,
        IPersonsViewModelToPersonEntityConverter
    {
        public PersonsViewModelToPersonEntityConverter(
            IEnumerable<IPersonsViewModelToPersonEntityPropertyConverter> propertyConverters)
            : base(propertyConverters)
        {
        }

        protected override PersonEntity InitializeDestination()
        {
            return new PersonEntity()
            {
                DNI = string.Empty,
                UserId = string.Empty,
            };
        }
    }
}