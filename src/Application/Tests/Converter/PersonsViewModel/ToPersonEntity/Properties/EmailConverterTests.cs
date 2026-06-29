// <copyright file="EmailConverterTests.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.Tests.Converter.PersonsViewModel.ToPersonEntity.Properties
{
    using Ricardo.MVCPrueba1.Application.Converter.PersonsViewModel.ToPersonEntity.Properties;
    using Ricardo.MVCPrueba1.Application.Models;
    using Ricardo.MVCPrueba1.Application.Tests.Constants;
    using Ricardo.MVCPrueba1.Application.Tests.Converter;
    using Ricardo.MVCPrueba1.Domain.Entities;

    /// <inheritdoc/>
    [TestClass]
    internal class EmailConverterTests : PropertyConverterPersonEntityTestBase<
        PersonViewModel,
        EmailConverter>
    {
        /// <inheritdoc/>
        protected override PersonEntity ExpectedValidResult()
        {
            PersonEntity personEntity = new PersonEntity()
            {
                UserId = string.Empty,
                DNI = string.Empty,
                Email = PersonConstants.Email,
            };

            return personEntity;
        }

        /// <inheritdoc/>
        protected override PersonViewModel ValidSource()
        {
            PersonViewModel personViewModel = new PersonViewModel()
            {
                Email = PersonConstants.Email,
            };

            return personViewModel;
        }
    }
}