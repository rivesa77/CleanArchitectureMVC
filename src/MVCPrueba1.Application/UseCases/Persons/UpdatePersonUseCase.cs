// <copyright file="UpdatePersonUseCase.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace MVCPrueba1.Application.UseCases.Persons
{
    using MVCPrueba1.Entities;
    using MVCPrueba1.Logic.Converter.PersonsViewModel.ToPersonEntity;
    using MVCPrueba1.Logic.Repositories;
    using MVCPrueba1.Logic.UserInfo;
    using MVCPrueba1.Models;
    using ROP;

    internal class UpdatePersonUseCase : IUpdatePersonUseCase
    {
        private readonly IPersonRepository personRepository;
        private readonly IPersonsViewModelToPersonEntityConverter converter;
        private readonly IPersonUserDetails personUserDetails;

        public UpdatePersonUseCase(
            IPersonRepository personRepository,
            IPersonsViewModelToPersonEntityConverter converter,
            IPersonUserDetails personUserDetails)
        {
            this.personRepository = personRepository;
            this.converter = converter;
            this.personUserDetails = personUserDetails;
        }

        public async Task<Result<bool>> Execute(PersonViewModel sourceClass)
        {
            if (sourceClass?.Id is null || string.IsNullOrWhiteSpace(sourceClass?.DNI))
            {
                return Result.Failure<bool>("The person can't be updated");
            }

            bool flagExist = await this.personRepository
                .ExistsByDniAndIdAsync(sourceClass.DNI, sourceClass.Id)
                .ConfigureAwait(false);

            if (flagExist)
            {
                return Result.Failure<bool>("Person DNI Already Exist");
            }

            PersonEntity personEntity = this.converter.Convert(sourceClass);

            bool result = await this.personRepository.UpdatePersonAsync(personEntity).ConfigureAwait(false);

            return Result.Success(result);
        }
    }
}