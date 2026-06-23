// <copyright file="UpdatePersonController.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Controllers.Persons
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Ricardo.MVCPrueba1.Application.Models;
    using Ricardo.MVCPrueba1.Application.UseCases.Persons.Updates;
    using ROP;

    [Authorize]
    [Route("[controller]")]
    public class UpdatePersonController : Controller
    {
        private readonly IUpdatePersonUseCase updatePersonUseCase;

        public UpdatePersonController(IUpdatePersonUseCase updatePersonUseCase)
        {
            this.updatePersonUseCase = updatePersonUseCase;
        }

        [HttpPost("{personId:guid}")]
        public async Task<IActionResult> UpdatePerson(Guid personId, PersonViewModel personViewModel)
        {
            personViewModel.Id = personId;

            Result<bool> result = await this.updatePersonUseCase.Execute(personViewModel)
                .ConfigureAwait(false);

            if (result.Errors.Any())
            {
                personViewModel.ErrorMessage = result.Errors.First().Message;

                return this.View("~/Views/Persons/SinglePerson.cshtml", personViewModel);
            }

            this.ViewBag.UpdateResult = true;

            return this.View("~/Views/Persons/SinglePerson.cshtml", personViewModel);
        }
    }
}
