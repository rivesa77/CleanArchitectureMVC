// <copyright file="RegisterClassAndPropertyConverterTestsBase.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.CleanArchitectureMVC.Application.Tests.Converters
{
    using FluentAssertions;
    using Microsoft.Extensions.DependencyInjection;

    [TestCategory("RegisterConverterAndPropertyConverter")]
    public abstract class RegisterClassAndPropertyConverterTestsBase
    {
        protected static void AssertRegisterPropertyConverters<TConverter>(Action<ServiceCollection> addServiceCollection)
        {
            Type propertyConverterType = typeof(TConverter);

            Type[] expectedConverterTypes = [.. propertyConverterType
                .Assembly
                .GetTypes()
                .Where(type => propertyConverterType.IsAssignableFrom(type)
                    && type.IsClass
                    && !type.IsAbstract)];

            ServiceCollection services = new ServiceCollection();

            addServiceCollection(services);

            Type[] registeredConverterTypes = [.. services
                .Where(service => service.ServiceType == propertyConverterType)
                .Select(service => service.ImplementationType)
                .Where(type => type is not null)];

            registeredConverterTypes
                .Should()
                .OnlyHaveUniqueItems();

            registeredConverterTypes
                .Should()
                .BeEquivalentTo(expectedConverterTypes);
        }
    }
}