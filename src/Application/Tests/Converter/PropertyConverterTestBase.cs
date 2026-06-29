// <copyright file="PropertyConverterTestBase.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.Tests.Converter
{
    using FluentAssertions;
    using Ricardo.CommonLibraries.Converters;

    [TestClass]
    public abstract class PropertyConverterTestBase<
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
        public void Converter_WithValidData_ReturnExpectedResult()
        {
            // Arrage.
            this.InitialiceCustomMocks();

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

        protected abstract TSourceClass ValidSource();

        protected abstract TDestinationClass ValidResult();

        protected abstract TDestinationClass ExpectedValidResult();

        protected virtual void InitialiceCustomMocks()
        {
        }

        protected virtual TPropertyConverter InitializePropertyConverter()
        {
            return new TPropertyConverter();
        }
    }
}