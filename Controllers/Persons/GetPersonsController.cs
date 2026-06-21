// <copyright file="GetPersonsController.cs" company="Ricardo">
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
    public class GetPersonsController : Controller
    {
        private readonly IGetPersonsUseCase getPersonsUseCase;

        public GetPersonsController(IGetPersonsUseCase getPersonsUseCase)
        {
            this.getPersonsUseCase = getPersonsUseCase;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            Result<IEnumerable<PersonViewModel>> result = await this.getPersonsUseCase.Execute()
                .ConfigureAwait(false);

            PersonCollectionViewModel personCollectionViewModel = new PersonCollectionViewModel();

            if (!result.Errors.Any())
            {
                personCollectionViewModel.Persons = result.Value;
            }

            return this.View("~/Views/Persons/Index.cshtml", personCollectionViewModel);
        }
    }
}