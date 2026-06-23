// <copyright file="GetPersonController.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Controllers.Persons
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Ricardo.MVCPrueba1.Application.Models;
    using Ricardo.MVCPrueba1.Application.UseCases.Persons.Gets;
    using ROP;

    [Authorize]
    [Route("Persons")]
    public class GetPersonController : Controller
    {
        private readonly IGetPersonUseCase getPersonUseCase;

        public GetPersonController(IGetPersonUseCase getPersonUseCase)
        {
            this.getPersonUseCase = getPersonUseCase;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPerson(Guid id)
        {
            Result<PersonViewModel> result = await this.getPersonUseCase.Execute(id)
                .ConfigureAwait(false);

            PersonViewModel personViewModel = result.Value ?? new PersonViewModel();

            if (result.Errors.Any())
            {
                personViewModel.ErrorMessage = result.Errors.First().Message;
            }

            return this.View("~/Views/Persons/SinglePerson.cshtml", personViewModel);
        }
    }
}
