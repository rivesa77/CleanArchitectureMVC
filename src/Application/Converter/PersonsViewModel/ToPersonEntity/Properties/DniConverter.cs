// <copyright file="DniConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.Converter.PersonsViewModel.ToPersonEntity.Properties
{
    using Ricardo.CommonLibraries.Converters;
    using Ricardo.MVCPrueba1.Application.Models;
    using Ricardo.MVCPrueba1.Domain.Entities;

    internal class DniConverter : ClassPropertyConverterBase<
        PersonViewModel,
        PersonEntity,
        string>,
        IPersonsViewModelToPersonEntityPorpertyConverter
    {
        protected override string GetPropertyValue(PersonViewModel source)
        {
            return source.DNI;
        }

        protected override void SetPropertyValue(in PersonEntity result, string propertyValue)
        {
            result.DNI = propertyValue.ToUpperInvariant();
        }
    }
}