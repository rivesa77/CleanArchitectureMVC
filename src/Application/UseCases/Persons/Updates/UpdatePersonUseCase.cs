// <copyright file="UpdatePersonUseCase.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.UseCases.Persons.Updates
{
    using Ricardo.MVCPrueba1.Application.Converter.PersonsViewModel.ToPersonEntity;
    using Ricardo.MVCPrueba1.Application.Models;
    using Ricardo.MVCPrueba1.Application.Models.Extensions;
    using Ricardo.MVCPrueba1.Application.Repositories;
    using Ricardo.MVCPrueba1.Application.UserInfo;
    using Ricardo.MVCPrueba1.Domain.Entities;
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
            Result<bool> validationResult = Validate(sourceClass);

            if (validationResult.Errors.Any())
            {
                return validationResult;
            }

            PersonEntity currentPerson = await this.personRepository
                .GetByIdAndUserIdAsync(sourceClass.Id, this.personUserDetails.UserId)
                .ConfigureAwait(false);

            if (currentPerson is null)
            {
                return Result.Failure<bool>("Person not found");
            }

            if (!sourceClass.HasChanges(currentPerson))
            {
                return Result.Failure<bool>("No changes to update");
            }

            bool hasDniChanged = !string.Equals(sourceClass.DNI, currentPerson.DNI, StringComparison.Ordinal);

            bool existsDniDb = await this.personRepository
                .ExistsByDniAndIdAsync(sourceClass.DNI, sourceClass.Id)
                .ConfigureAwait(false);

            if (hasDniChanged && existsDniDb)
            {
                return Result.Failure<bool>("Person DNI Already Exist");
            }

            PersonEntity personEntity = this.converter.Convert(sourceClass);

            bool result = await this.personRepository.UpdatePersonAsync(personEntity).ConfigureAwait(false);

            return Result.Success(result);
        }

        private static Result<bool> Validate(PersonViewModel sourceClass)
        {
            if (sourceClass?.Id is null || string.IsNullOrWhiteSpace(sourceClass?.DNI))
            {
                return Result.Failure<bool>("The person can't be updated");
            }

            if (sourceClass.DNI.Length != 9)
            {
                return Result.Failure<bool>("Person DNI must have 9 characters");
            }

            if (string.IsNullOrWhiteSpace(sourceClass.Name))
            {
                return Result.Failure<bool>("Person name is required");
            }

            if (sourceClass.Name.Length > 100)
            {
                return Result.Failure<bool>("Person name can't have more than 100 characters");
            }

            return Result.Success(true);
        }
    }
}