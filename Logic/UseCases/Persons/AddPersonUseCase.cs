// <copyright file="AddPersonUseCase.cs" company="Ricardo">
// Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace MVCPrueba1.Logic.UseCases.Persons
{
    using Microsoft.EntityFrameworkCore;
    using MVCPrueba1.Data;
    using MVCPrueba1.Entities;
    using MVCPrueba1.Logic.UserInfo;
    using MVCPrueba1.Models;
    using ROP;

    public class AddPersonUseCase : IAddPersonUseCase
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IPersonUserDetails personUserDetails;

        public AddPersonUseCase(
            ApplicationDbContext applicationDbContext,
            IPersonUserDetails flagUserDetails)
        {
            this.applicationDbContext = applicationDbContext;
            this.personUserDetails = flagUserDetails;
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
            PersonEntity personEntity = new PersonEntity()
            {
                DNI = personViewModel.DNI,
                UserId = this.personUserDetails.UserId,
                Email = personViewModel.Email,
                Name = personViewModel.Name,
                Phone = personViewModel.Phone,
            };

            await this.applicationDbContext.AddAsync(personEntity);
            await this.applicationDbContext.SaveChangesAsync();

            return true;
        }
    }
}