// <copyright file="GetPersonsUseCase.cs" company="Ricardo">
// Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace MVCPrueba1.Logic.UseCases.Persons
{
    using Microsoft.EntityFrameworkCore;
    using MVCPrueba1.Data;
    using MVCPrueba1.Logic.UserInfo;
    using MVCPrueba1.Models;
    using ROP;

    public class GetPersonsUseCase : IGetPersonsUseCase
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IPersonUserDetails personUserDetails;

        public GetPersonsUseCase(ApplicationDbContext applicationDbContext, IPersonUserDetails personUserDetails)
        {
            this.applicationDbContext = applicationDbContext;
            this.personUserDetails = personUserDetails;
        }

        public async Task<Result<IEnumerable<PersonViewModel>>> Execute()
        {
            return await this.GetPersonsFromDatabase();
        }

        private async Task<Result<IEnumerable<PersonViewModel>>> GetPersonsFromDatabase()
        {
            string userId = this.personUserDetails.UserId;

            IEnumerable<PersonViewModel> persons = await this.applicationDbContext.Persons
                .Where(p => p.UserId == userId)
                .Select(p => new PersonViewModel
                {
                    DNI = p.DNI,
                    Name = p.Name,
                    Email = p.Email,
                    Phone = p.Phone,
                    Id = p.Id,
                })
                .AsNoTracking()
                .ToListAsync();

            return Result.Success(persons);
        }
    }
}