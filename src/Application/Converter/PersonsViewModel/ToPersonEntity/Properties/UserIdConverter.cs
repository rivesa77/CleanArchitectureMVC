// <copyright file="UserIdConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.Converter.PersonsViewModel.ToPersonEntity.Properties
{
    using Ricardo.Application.Models;
    using Ricardo.Application.UserInfo;
    using Ricardo.CommonLibraries.Converters;
    using Ricardo.Domain.Entities;

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