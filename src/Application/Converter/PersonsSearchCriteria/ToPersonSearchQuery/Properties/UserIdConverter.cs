// <copyright file="UserIdConverter.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.Converter.PersonsSearchCriteria.ToPersonSearchQuery.Properties
{
    using Ricardo.CommonLibraries.Converters;
    using Ricardo.MVCPrueba1.Application.Repositories;
    using Ricardo.MVCPrueba1.Application.UseCases.Persons.Searches;
    using Ricardo.MVCPrueba1.Application.UserInfo;

    /// <inheritdoc/>
    internal class UserIdConverter : ClassPropertyConverterBase<
        PersonSearchCriteria,
        PersonSearchQuery,
        string>,
        IPersonsSearchCriteriaToPersonSearchQueryPropertyConverter
    {
        private readonly IPersonUserDetails personUserDetails;

        public UserIdConverter(IPersonUserDetails personUserDetails)
        {
            this.personUserDetails = personUserDetails;
        }

        /// <inheritdoc/>
        protected override string GetPropertyValue(PersonSearchCriteria source)
        {
            return this.personUserDetails.UserId;
        }

        /// <inheritdoc/>
        protected override void SetPropertyValue(in PersonSearchQuery result, string propertyValue)
        {
            result.UserId = propertyValue;
        }
    }
}