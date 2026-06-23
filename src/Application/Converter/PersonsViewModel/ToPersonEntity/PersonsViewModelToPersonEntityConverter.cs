// <copyright file="PersonsViewModelToPersonEntityConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.Converter.PersonsViewModel.ToPersonEntity
{
    using Ricardo.MVCPrueba1.Application.Converter.PersonsViewModel.ToPersonEntity.Properties;
    using Ricardo.MVCPrueba1.Application.Models;
    using Ricardo.MVCPrueba1.Domain.Entities;

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