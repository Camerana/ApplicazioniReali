using ApplicazioniReali.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicazioniReali.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //public string Index(string name, string lastname)
        //{
        //    return $"Ciao, {name} {lastname}";
        //    //return "Ciao, " + name + " " + lastname;
        //    //return string.Concat("Ciao, ", name, " ", lastname);
        //}

        public IActionResult Index(string name, string lastname)
        {
            object message = $"{name} {lastname}";
            return View(message);
        }

        public IActionResult Privacy()
        {
            object htmlFromDb = $"<h1>Ciao, sono un tag H1 che arriva dal DB. Come mi vedi?</h1>";
            return View(htmlFromDb);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
