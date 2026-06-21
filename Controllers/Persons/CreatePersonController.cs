// <copyright file="CreatePersonController.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace MVCPrueba1.Controllers.Persons
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MVCPrueba1.Logic.UseCases.Persons;
    using MVCPrueba1.Models;
    using ROP;

    [Authorize]
    [Route("Persons")]
    public class CreatePersonController : Controller
    {
        private readonly IAddPersonUseCase addPersonUseCase;

        public CreatePersonController(IAddPersonUseCase addPersonUseCase)
        {
            this.addPersonUseCase = addPersonUseCase;
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            PersonViewModel personViewModel = new PersonViewModel();

            return this.View("~/Views/Persons/Create.cshtml", personViewModel);
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddPersonToDataBase(PersonViewModel personViewModel)
        {
            Result<bool> result = await this.addPersonUseCase.Execute(personViewModel)
                .ConfigureAwait(false);

            if (result.Errors.Any())
            {
                personViewModel.ErrorMessage = result.Errors.First().Message;

                return this.View("~/Views/Persons/Create.cshtml", personViewModel);
            }

            return this.RedirectToAction("Index", "GetPersons");
        }
    }
}