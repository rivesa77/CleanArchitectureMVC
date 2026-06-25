// <copyright file="GetPersonsController.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Controllers.Persons
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Ricardo.MVCPrueba1.Application.Models;
    using Ricardo.MVCPrueba1.Application.UseCases.Persons.Searches;
    using ROP;

    [Authorize]
    [Route("Persons")]
    public class GetPersonsController : Controller
    {
        private readonly ISearchPersonsUseCase searchPersonsUseCase;

        public GetPersonsController(ISearchPersonsUseCase searchPersonsUseCase)
        {
            this.searchPersonsUseCase = searchPersonsUseCase;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index(
            PersonSearchField searchField = PersonSearchField.All,
            string searchTerm = default,
            int pageNumber = 1,
            int pageSize = 5)
        {
            PersonSearchCriteria criteria = new PersonSearchCriteria()
            {
                SearchField = searchField,
                SearchTerm = searchTerm,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            Result<PersonSearchViewModel> result = await this.searchPersonsUseCase.Execute(criteria)
                .ConfigureAwait(false);

            PersonSearchViewModel personSearchViewModel = new PersonSearchViewModel();

            if (!result.Errors.Any())
            {
                personSearchViewModel = result.Value;
            }

            return this.View("~/Views/Persons/Index.cshtml", personSearchViewModel);
        }
    }
}
