// <copyright file="AddPersonUseCase.cs" company="Ricardo">
// Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace MVCPrueba1.Logic.UseCases.Persons
{
    using Microsoft.EntityFrameworkCore;
    using MVCPrueba1.Data;
    using MVCPrueba1.Entities;
    using MVCPrueba1.Logic.Converter.PersonsViewModel.ToPersonEntity;
    using MVCPrueba1.Logic.UserInfo;
    using MVCPrueba1.Models;
    using ROP;

    internal class AddPersonUseCase : IAddPersonUseCase
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IPersonUserDetails personUserDetails;
        private readonly IPersonsViewModelToPersonEntityConverter converter;

        public AddPersonUseCase(
            ApplicationDbContext applicationDbContext,
            IPersonUserDetails flagUserDetails,
            IPersonsViewModelToPersonEntityConverter converter)
        {
            this.applicationDbContext = applicationDbContext;
            this.personUserDetails = flagUserDetails;
            this.converter = converter;
        }

        public async Task<Result<bool>> Execute(PersonViewModel personViewModel)
        {
            if (string.IsNullOrWhiteSpace(personViewModel.DNI))
            {
                return Result.Failure<bool>("Person DNI is required");
            }

            return await this.ValidatePerson(personViewModel).Bind(x => this.AddPersonToDatabase(personViewModel)).ConfigureAwait(false);
        }

        private async Task<Result<bool>> ValidatePerson(PersonViewModel personViewModel)
        {
            bool flagExist = await this.applicationDbContext.Persons
                .AnyAsync(p => p.DNI.ToLower() == personViewModel.DNI.ToLower())
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

            await this.applicationDbContext.AddAsync(personEntity);
            await this.applicationDbContext.SaveChangesAsync();

            return true;
        }
    }
}