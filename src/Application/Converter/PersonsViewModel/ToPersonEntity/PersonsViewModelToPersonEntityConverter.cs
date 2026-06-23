// <copyright file="PersonsViewModelToPersonEntityConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.Converter.PersonsViewModel.ToPersonEntity
{
    using Ricardo.Application.Converter.PersonsViewModel.ToPersonEntity.Properties;
    using Ricardo.Application.Models;
    using Ricardo.Domain.Entities;

    internal class PersonsViewModelToPersonEntityConverter :
        IPersonsViewModelToPersonEntityConverter
    {
        private readonly IEnumerable<IPersonsViewModelToPersonEntityPorpertyConverter> propertyConverters;

        public PersonsViewModelToPersonEntityConverter(
            IEnumerable<IPersonsViewModelToPersonEntityPorpertyConverter> propertyConverters)
        {
            this.propertyConverters = propertyConverters;
        }

        public PersonEntity Convert(PersonViewModel source)
        {
            PersonEntity destination = new()
            {
                DNI = string.Empty,
                UserId = string.Empty,
            };

            this.Convert(source, ref destination);

            return destination;
        }

        public void Convert(PersonViewModel source, ref PersonEntity destination)
        {
            foreach (IPersonsViewModelToPersonEntityPorpertyConverter propertyConverter in this.propertyConverters)
            {
                propertyConverter.Convert(source, in destination);
            }
        }
    }
}