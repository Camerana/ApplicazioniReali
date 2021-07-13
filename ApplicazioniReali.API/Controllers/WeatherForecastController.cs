using ApplicazioniReali.Db.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicazioniReali.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ApplicazionirealiContext _context;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        //  https:localhost:4332/WeatherForecast
        public WeatherForecastController(ILogger<WeatherForecastController> logger, ApplicazionirealiContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET =>  https:localhost:4332/WeatherForecast
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        // GET =>  https:localhost:4332/WeatherForecast/get2
        [HttpGet("get2")]
        public IEnumerable<Db.Models.Movie> GetMovie()
        {
            var movies = _context.Movies.ToList();

            return movies;
        }
    }
}
