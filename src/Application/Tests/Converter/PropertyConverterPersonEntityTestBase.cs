// <copyright file="PropertyConverterPersonEntityTestBase.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.Tests.Converter
{
    using Ricardo.CommonLibraries.Converters;
    using Ricardo.MVCPrueba1.Domain.Entities;

    [TestCategory("PropertyConverterPersonEntity")]
    internal abstract class PropertyConverterPersonEntityTestBase<TSourceClass, TPropertyConverter> : PropertyConverterWithRequiredFieldTestBase<
        TSourceClass,
        PersonEntity,
        TPropertyConverter>
        where TSourceClass : class, new()
        where TPropertyConverter : class, IClassPropertyConverter<TSourceClass, PersonEntity>, new()
    {
        private static readonly PersonEntity EmptyPersonEntity = new PersonEntity()
        {
            UserId = string.Empty,
            DNI = string.Empty,
        };

        /// <inheritdoc/>
        protected override PersonEntity ValidResult()
        {
            return EmptyPersonEntity;
        }

        /// <inheritdoc/>
        protected override PersonEntity EmptyDestinationClass()
        {
            return EmptyPersonEntity;
        }

        /// <inheritdoc/>
        protected override TSourceClass EmptySourceClass()
        {
            return new TSourceClass();
        }

        protected override PersonEntity ExpectedEmptyDestinationClass()
        {
            return EmptyPersonEntity;
        }
    }
}