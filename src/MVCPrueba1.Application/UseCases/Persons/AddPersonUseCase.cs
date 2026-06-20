// <copyright file="AddPersonUseCase.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace MVCPrueba1.Logic.UseCases.Persons
{
    using MVCPrueba1.Entities;
    using MVCPrueba1.Logic.Converter.PersonsViewModel.ToPersonEntity;
    using MVCPrueba1.Logic.Repositories;
    using MVCPrueba1.Models;
    using ROP;

    internal class AddPersonUseCase : IAddPersonUseCase
    {
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
            if (string.IsNullOrWhiteSpace(personViewModel.DNI))
            {
                return Result.Failure<bool>("Person DNI is required");
            }

            return await this.ValidatePerson(personViewModel)
                .Bind(x => this.AddPersonToDatabase(personViewModel))
                .ConfigureAwait(false);
        }

        private async Task<Result<bool>> ValidatePerson(PersonViewModel personViewModel)
        {
            bool flagExist = await this.personRepository
                .ExistsByDniAsync(personViewModel.DNI)
                .ConfigureAwait(false);

            if (flagExist)
            {
                return Result.Failure<bool>("Person DNI Already Exist");
            }

            return true;
        }

        private async Task<Result<bool>> AddPersonToDatabase(PersonViewModel personViewModel)
        {
            PersonEntity personEntity = this.converter.Convert(personViewModel);

            await this.personRepository.AddAsync(personEntity).ConfigureAwait(false);

            return true;
        }
    }
}