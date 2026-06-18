// <copyright file="PersonsController.cs" company="Ricardo">
// Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace MVCPrueba1.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MVCPrueba1.Models;

    [Authorize]
    [Route("[controller]")]
    public class PersonsController : Controller
    {
        [HttpGet("create")]
        public IActionResult Create()
        {
            PersonViewModel personViewModel = new PersonViewModel();

            return this.View(personViewModel);
        }
    }
}