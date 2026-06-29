// <copyright file="PropertyConverterWithRequiredFieldTestBase.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.Tests.Converter
{
    using FluentAssertions;
    using Ricardo.CommonLibraries.Converters;

    [TestCategory("Converter")]
    [TestClass]
    public abstract class PropertyConverterWithRequiredFieldTestBase<
        TSourceClass,
        TDestinationClass,
        TPropertyConverter>
        where TSourceClass : class
        where TDestinationClass : class
        where TPropertyConverter : class, IClassPropertyConverter<TSourceClass, TDestinationClass>, new()
    {
        [TestInitialize]
        public virtual void Initialize()
        {
        }

        [TestCleanup]
        public virtual void Verify()
        {
        }

        [TestMethod]
        public void Converter_WithValidData_ReturnValidResult()
        {
            // Arrange.
            TPropertyConverter propertyConverter = this.InitializePropertyConverter();

            TSourceClass sourceClass = this.ValidSource();

            TDestinationClass result = this.ValidResult();

            // Act.
            propertyConverter.Convert(sourceClass, result);

            TDestinationClass expectedValidResult = this.ExpectedValidResult();

            // Assert.
            result
                .Should()
                .BeEquivalentTo(expectedValidResult);
        }

        [TestMethod]
        public void Converter_WithNullData_ReturnEmptyResult()
        {
            // Arrange.
            TPropertyConverter propertyConverter = this.InitializePropertyConverter();

            TSourceClass sourceClass = this.EmptySourceClass();

            TDestinationClass result = this.EmptyDestinationClass();

            // Act.
            propertyConverter.Convert(sourceClass, result);

            TDestinationClass expectedValidResult = this.ExpectedEmptyDestinationClass();

            // Assert.
            result
                .Should()
                .BeEquivalentTo(expectedValidResult);
        }

        protected abstract TSourceClass ValidSource();

        protected abstract TDestinationClass ValidResult();

        protected abstract TDestinationClass ExpectedValidResult();

        protected abstract TSourceClass EmptySourceClass();

        protected abstract TDestinationClass EmptyDestinationClass();

        protected abstract TDestinationClass ExpectedEmptyDestinationClass();

        protected virtual TPropertyConverter InitializePropertyConverter()
        {
            return new TPropertyConverter();
        }
    }
}