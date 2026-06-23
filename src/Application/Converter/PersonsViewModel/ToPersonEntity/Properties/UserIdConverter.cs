// <copyright file="UserIdConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.Converter.PersonsViewModel.ToPersonEntity.Properties
{
    using Ricardo.CommonLibraries.Converters;
    using Ricardo.MVCPrueba1.Application.Models;
    using Ricardo.MVCPrueba1.Application.UserInfo;
    using Ricardo.MVCPrueba1.Domain.Entities;

    internal class UserIdConverter : ClassPropertyConverterBase<
        PersonViewModel,
        PersonEntity,
        string>,
        IPersonsViewModelToPersonEntityPorpertyConverter
    {
        private readonly IPersonUserDetails personUserDetails;

        public UserIdConverter(IPersonUserDetails personUserDetails)
        {
            this.personUserDetails = personUserDetails;
        }

        protected override string GetPropertyValue(PersonViewModel source)
        {
            return this.personUserDetails.UserId;
        }

        protected override void SetPropertyValue(in PersonEntity result, string propertyValue)
        {
            result.UserId = propertyValue;
        }
    }
}