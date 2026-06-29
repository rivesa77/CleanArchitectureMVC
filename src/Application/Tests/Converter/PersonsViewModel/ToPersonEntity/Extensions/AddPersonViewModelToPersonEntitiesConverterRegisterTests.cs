// <copyright file="AddPersonViewModelToPersonEntitiesConverterRegisterTests.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.Tests.Converter.PersonsViewModel.ToPersonEntity.Extensions
{
    using Ricardo.MVCPrueba1.Application.Converter.PersonsViewModel.ToPersonEntity;
    using Ricardo.MVCPrueba1.Application.Converter.PersonsViewModel.ToPersonEntity.Extensions;
    using Ricardo.MVCPrueba1.Application.Tests.Converter;

    /// <inheritdoc/>
    [TestClass]
    [TestCategory("RegistersConverters")]
    public class AddPersonViewModelToPersonEntitiesConverterRegisterTests :
        RegisterPropertyConverterTestsBase
    {
        [TestMethod]
        public void AddPersonViewModelToPersonEntitiesConverter_WhenCalled_RegistersConverters()
        {
            AssertRegisterPropertyConverters<IPersonsViewModelToPersonEntityConverter>(
                services => services.AddPersonViewModelToPersonEntitiesConverter());
        }
    }
}