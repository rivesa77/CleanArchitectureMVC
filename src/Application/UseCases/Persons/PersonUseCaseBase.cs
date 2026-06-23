// <copyright file="PersonUseCaseBase.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Application.UseCases.Persons
{
    using Ricardo.Application.Repositories;
    using Ricardo.Application.UserInfo;

    internal class PersonUseCaseBase
    {
        protected IPersonRepository PersonRepository { get; }

        protected IPersonUserDetails PersonUserDetails { get; }

        public PersonUseCaseBase(IPersonRepository personRepository, IPersonUserDetails personUserDetails)
        {
            this.PersonRepository = personRepository;
            this.PersonUserDetails = personUserDetails;
        }
    }
}