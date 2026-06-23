// <copyright file="AddPersonUseCase.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Application.UseCases.Persons.Creates
{
    using Ricardo.MVCPrueba1.Application.Converter.PersonsViewModel.ToPersonEntity;
    using Ricardo.MVCPrueba1.Application.Models;
    using Ricardo.MVCPrueba1.Application.Repositories;
    using Ricardo.MVCPrueba1.Domain.Entities;
    using ROP;

    internal class AddPersonUseCase : IAddPersonUseCase
    {
        private const string DniRequiredMessage = "Person DNI is required";
        private const string DniAlreadyExistMessage = "Person DNI Already Exist";
        private const string ConverterErrorMessage = "Conversion from PersonViewModel to PersonEntity failed, PersonEntity is null";

        private readonly IPersonRepository personRepository;
        private readonly IPersonsViewModelToPersonEntityConverter converter;

        public AddPersonUseCase(
            IPersonRepository personRepository,
            IPersonsViewModelToPersonEntityConverter converter)
        {
            this.personRepository = personRepository;
            this.converter = converter;
        }

        public async Task<Result<bool>> Execute(PersonViewModel personViewModel)
        {
            if (string.IsNullOrWhiteSpace(personViewModel?.DNI))
            {
                return Result.Failure<bool>(DniRequiredMessage);
            }

            return await this.ValidatePerson(personViewModel)
                .Bind(x => this.AddPersonToDatabase(personViewModel))
                .ConfigureAwait(false);
        }

        private async Task<Result<bool>> ValidatePerson(PersonViewModel personViewModel)
        {
            bool personExist = await this.personRepository
                .ExistsByDniAsync(personViewModel.DNI)
                .ConfigureAwait(false);

            if (personExist)
            {
                return Result.Failure<bool>(DniAlreadyExistMessage);
            }

            return true;
        }

        private async Task<Result<bool>> AddPersonToDatabase(PersonViewModel personViewModel)
        {
            PersonEntity personEntity = this.converter.Convert(personViewModel);

            if (personEntity is null)
            {
                return Result.Failure<bool>(ConverterErrorMessage);
            }

            bool result = await this.personRepository.AddAsync(personEntity).ConfigureAwait(false);

            return result;
        }
    }
}