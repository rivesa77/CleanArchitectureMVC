namespace MVCPrueba1.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MVCPrueba1.Models;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("example/{text}")]
        public IActionResult Example(string text)
        {
            IndexViewModel indexViewModel = new()
            {
                Content = text
            };

            return View("Example", indexViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}