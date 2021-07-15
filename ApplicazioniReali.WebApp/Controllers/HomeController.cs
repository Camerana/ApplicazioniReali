using ApplicazioniReali.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApplicazioniReali.Db.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicazioniReali.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicazionirealiContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicazionirealiContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var allData = new AllData()
            {
                Users = _context.Users.ToList(),
                Movies= _context.Movies.ToList()
            };    

            return View(allData);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
