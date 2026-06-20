// <copyright file="DeletePersonUseCase.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace MVCPrueba1.Application.UseCases.Persons
{
    using MVCPrueba1.Entities;
    using MVCPrueba1.Logic.Converter.PersonsViewModel.ToPersonEntity;
    using MVCPrueba1.Logic.Repositories;
    using MVCPrueba1.Models;
    using ROP;

    internal class DeletePersonUseCase : IDeletePersonUseCase
    {
        private readonly IPersonRepository personRepository;
        private readonly IPersonsViewModelToPersonEntityConverter converter;

        public DeletePersonUseCase(IPersonRepository personRepository, IPersonsViewModelToPersonEntityConverter converter)
        {
            this.personRepository = personRepository;
            this.converter = converter;
        }

        public async Task<Result<bool>> Execute(PersonViewModel sourceClass)
        {
            if (sourceClass is null)
            {
                return Result.Failure<bool>("sourceClass is required");
            }

            PersonEntity personEntity = this.converter.Convert(sourceClass);

            return await this.personRepository.DeletePersonAsync(personEntity);
        }
    }
}