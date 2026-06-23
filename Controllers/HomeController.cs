// <copyright file="HomeController.cs" company="Ricardo">
//     Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.MVCPrueba1.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Ricardo.MVCPrueba1.Application.Models;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [HttpGet("example/{text}")]
        public IActionResult Example(string text)
        {
            IndexViewModel indexViewModel = new IndexViewModel()
            {
                Content = text,
            };

            return this.View("Example", indexViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}