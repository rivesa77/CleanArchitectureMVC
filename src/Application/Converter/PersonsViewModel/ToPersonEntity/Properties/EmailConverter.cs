// <copyright file="EmailConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.Converter.PersonsViewModel.ToPersonEntity.Properties
{
    using Ricardo.CommonLibraries.Converters;
    using Ricardo.MVCPrueba1.Application.Models;
    using Ricardo.MVCPrueba1.Domain.Entities;

    internal class EmailConverter : ClassPropertyConverterBase<
        PersonViewModel,
        PersonEntity,
        string>,
        IPersonsViewModelToPersonEntityPorpertyConverter
    {
        protected override string GetPropertyValue(PersonViewModel source)
        {
            return source.Email;
        }

        protected override void SetPropertyValue(in PersonEntity result, string propertyValue)
        {
            result.Email = propertyValue;
        }
    }
}