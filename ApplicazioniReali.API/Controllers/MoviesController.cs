using ApplicazioniReali.Db.Data;
using ApplicazioniReali.Db.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicazioniReali.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicazionirealiContext _context;

        public MoviesController(ApplicazionirealiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            var items = _context.Movies.ToList();
            return items;
        }

        [HttpGet("{id}")]
        public Movie Get(Guid id)
        {
            var item = _context.Movies.Find(id);
            return item;
        }

        [HttpPost]
        public IActionResult Post(Movie user)
        {
            _context.Add(user);
            _context.SaveChanges();
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
                _context.Update(user);
                _context.SaveChanges();
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
            var user = _context.Movies.Find(id);

            if (user == null)
                return NotFound();

            _context.Remove(user);
            _context.SaveChanges();
            return Ok("film_rimosso");
        }

        private bool UserExist(Guid id)
        {
            return _context.Movies.Any(x => x.Id == id);
        }
    }
}
