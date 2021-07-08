using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicazioniReali.API.Extensions;
using ApplicazioniReali.Db.Data;
using ApplicazioniReali.Db.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApplicazioniReali.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicazionirealiContext _applicazionirealiContext;

        public MoviesController(ApplicazionirealiContext applicazionirealiContext)
        {
            _applicazionirealiContext = applicazionirealiContext;
        }

        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            var items = _applicazionirealiContext.Movies.ToList();
            return items;
        }

        [HttpGet("{id}")]
        public Movie Get(Guid id)
        {
            var item = _applicazionirealiContext.Movies.Find(id);
            return item;
        }

        [HttpPost]
        public IActionResult Post(Movie user)
        {
            _applicazionirealiContext.Add(user);
            _applicazionirealiContext.SaveChanges();
            return Ok("film_creato");
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, Movie user)
        {
            if (id != user.Id)
                return BadRequest("id_non_uguale_al_modello");

            if (!UserExist(id))
                return NotFound();

            try
            {
                _applicazionirealiContext.Update(user);
                _applicazionirealiContext.SaveChanges();
                return Ok("film_aggiornato");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExist(id))
                    return NotFound();
                else
                    throw;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var user = _applicazionirealiContext.Movies.Find(id);

            if (user == null)
                return NotFound();

            _applicazionirealiContext.Remove(user);
            _applicazionirealiContext.SaveChanges();
            return Ok("film_rimosso");
        }

        private bool UserExist(Guid id)
        {
            return _applicazionirealiContext.Movies.Any(x => x.Id == id);
        }
    }
}
