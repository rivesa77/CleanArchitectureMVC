// <copyright file="DeletePersonController.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace MVCPrueba1.Controllers.Persons
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MVCPrueba1.Application.UseCases.Persons;
    using MVCPrueba1.Models;
    using ROP;

    [Authorize]
    [Route("[controller]")]
    public class DeletePersonController : Controller
    {
        private readonly IDeletePersonUseCase deletePersonUseCase;

        public DeletePersonController(IDeletePersonUseCase deletePersonUseCase)
        {
            this.deletePersonUseCase = deletePersonUseCase;
        }

        [HttpPost("delete/{personId:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePerson(Guid personId)
        {
            PersonViewModel personViewModel = new PersonViewModel
            {
                Id = personId,
            };

            Result<bool> isDeleted = await this.deletePersonUseCase.Execute(personViewModel)
                .ConfigureAwait(false);

            if (isDeleted.Success)
            {
                return this.RedirectToAction("Index", "GetPersons");
            }

            return this.View("~/Views/Persons/SinglePerson.cshtml", personViewModel);
        }
    }
}