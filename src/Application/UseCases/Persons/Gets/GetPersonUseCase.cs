// <copyright file="GetPersonUseCase.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.UseCases.Persons.Gets
{
    using Ricardo.MVCPrueba1.Application.Converter.PersonEntities.ToPersonViewModel;
    using Ricardo.MVCPrueba1.Application.Models;
    using Ricardo.MVCPrueba1.Application.Repositories;
    using Ricardo.MVCPrueba1.Application.UserInfo;
    using Ricardo.MVCPrueba1.Domain.Entities;
    using ROP;

    internal class GetPersonUseCase : PersonUseCaseBase, IGetPersonUseCase
    {
        private readonly IPersonEntitiesToPersonViewModelConverter converter;

        public GetPersonUseCase(
            IPersonRepository personRepository,
            IPersonUserDetails personUserDetails,
            IPersonEntitiesToPersonViewModelConverter converter)
            : base(personRepository, personUserDetails)
        {
            this.converter = converter;
        }

        public async Task<Result<PersonViewModel>> Execute(Guid sourceClass)
        {
            if (sourceClass == Guid.Empty)
            {
                return Result.Failure<PersonViewModel>("No data");
            }

            return await this.GetPerson(sourceClass).ConfigureAwait(false);
        }

        private async Task<Result<PersonViewModel>> GetPerson(Guid id)
        {
            PersonEntity person = await this.PersonRepository
                .GetByIdAndUserIdAsync(id, this.PersonUserDetails.UserId)
                .ConfigureAwait(false);

            if (person is null)
            {
                return Result.Failure<PersonViewModel>("Person not found");
            }

            return this.converter.Convert(person);
        }
    }
}