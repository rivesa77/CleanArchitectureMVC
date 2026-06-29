// <copyright file="PhoneConverterTests.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.Tests.Converter.PersonsViewModel.ToPersonEntity.Properties
{
    using Ricardo.MVCPrueba1.Application.Converter.PersonsViewModel.ToPersonEntity.Properties;
    using Ricardo.MVCPrueba1.Application.Models;
    using Ricardo.MVCPrueba1.Application.Tests.Constants;
    using Ricardo.MVCPrueba1.Application.Tests.Converter;
    using Ricardo.MVCPrueba1.Domain.Entities;

    /// <inheritdoc/>
    [TestClass]
    internal class PhoneConverterTests : PropertyConverterPersonEntityTestBase<
        PersonViewModel,
        PhoneConverter>
    {
        /// <inheritdoc/>
        protected override PersonEntity ExpectedValidResult()
        {
            PersonEntity personEntity = new PersonEntity()
            {
                UserId = string.Empty,
                DNI = string.Empty,
                Phone = PersonConstants.Phone,
            };

            return personEntity;
        }

        /// <inheritdoc/>
        protected override PersonViewModel ValidSource()
        {
            PersonViewModel personViewModel = new PersonViewModel()
            {
                Phone = PersonConstants.Phone,
            };

            return personViewModel;
        }
    }
}