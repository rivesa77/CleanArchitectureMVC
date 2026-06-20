// <copyright file="PersonsController.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace MVCPrueba1.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MVCPrueba1.Application.UseCases.Persons;
    using MVCPrueba1.Logic.UseCases.Persons;
    using MVCPrueba1.Models;
    using ROP;

    [Authorize]
    [Route("[controller]")]
    public class PersonsController : Controller
    {
        private readonly IAddPersonUseCase addPersonUseCase;
        private readonly IGetPersonsUseCase getPersonsUseCase;
        private readonly IGetPersonUseCase getPersonUseCase;
        private readonly IUpdatePersonUseCase updatePersonUseCase;

        public PersonsController(
            IAddPersonUseCase addPersonUseCase,
            IGetPersonsUseCase getPersonsUseCase,
            IGetPersonUseCase getPersonUseCase,
            IUpdatePersonUseCase updatePersonUseCase)
        {
            this.addPersonUseCase = addPersonUseCase;
            this.getPersonsUseCase = getPersonsUseCase;
            this.getPersonUseCase = getPersonUseCase;
            this.updatePersonUseCase = updatePersonUseCase;
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

            PersonCollectionViewModel personCollectionViewModel = new PersonCollectionViewModel();

            if (!result.Errors.Any())
            {
                personCollectionViewModel.Persons = result.Value;
            }

            return this.View("Index", personCollectionViewModel);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPerson(Guid id)
        {
            Result<PersonViewModel> result = await this.getPersonUseCase.Execute(id)
                .ConfigureAwait(false);

            PersonViewModel personViewModel = result.Value;

            if (result.Errors.Any())
            {
                personViewModel.ErrorMessage = result.Errors.First().Message;
            }

            return this.View("SinglePerson", personViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePerson(PersonViewModel personViewModel)
        {
            Result<bool> result = await this.updatePersonUseCase.Execute(personViewModel)
                .ConfigureAwait(false);

            if (result.Errors.Any())
            {
                personViewModel.ErrorMessage = result.Errors.First().Message;

                return this.View("SinglePerson", personViewModel);
            }

            this.ViewBag.UpdateResult = true;

            return this.View("SinglePerson", personViewModel);
        }
    }
}