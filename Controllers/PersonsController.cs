// <copyright file="PersonsController.cs" company="Ricardo">
// Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace MVCPrueba1.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MVCPrueba1.Logic.UseCases.Persons;
    using MVCPrueba1.Models;
    using ROP;

    [Authorize]
    [Route("[controller]")]
    public class PersonsController : Controller
    {
        private readonly IAddPersonUseCase addPersonUseCase;
        private readonly IGetPersonsUseCase getPersonsUseCase;

        public PersonsController(
            IAddPersonUseCase addPersonUseCase,
            IGetPersonsUseCase getPersonsUseCase)
        {
            this.addPersonUseCase = addPersonUseCase;
            this.getPersonsUseCase = getPersonsUseCase;
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            PersonViewModel personViewModel = new PersonViewModel();

            return this.View(personViewModel);
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddPersonToDataBase(PersonViewModel personViewModel)
        {
            Result<bool> result = await this.addPersonUseCase.Execute(personViewModel)
                .ConfigureAwait(false);

            if (result.Errors.Any())
            {
                personViewModel.ErrorMessage = result.Errors.First().Message;

                return this.View("Create", personViewModel);
            }

            return this.RedirectToAction("Index");
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            Result<IEnumerable<PersonViewModel>> result = await this.getPersonsUseCase.Execute()
                .ConfigureAwait(false);

            if (result.Errors.Any())
            {
                return this.View("Index", new List<PersonViewModel>());
            }

            PersonCollectionViewModel personCollectionViewModel = new PersonCollectionViewModel()
            {
                Persons = result.Value,
            };

            return this.View("Index", personCollectionViewModel);
        }
    }
}