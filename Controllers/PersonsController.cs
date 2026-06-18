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

        public PersonsController(IAddPersonUseCase addPersonUseCase)
        {
            this.addPersonUseCase = addPersonUseCase;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return this.View();
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
    }
}