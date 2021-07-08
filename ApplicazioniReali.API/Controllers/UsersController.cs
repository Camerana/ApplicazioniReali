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
    public class UsersController: ControllerBase
    {
        private readonly ApplicazionirealiContext _applicazionirealiContext;

        public UsersController(ApplicazionirealiContext applicazionirealiContext)
        {
            _applicazionirealiContext = applicazionirealiContext;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            var items = _applicazionirealiContext.Users.ToList();
            return items;
        }

        [HttpGet("{id}")]
        public User Get(Guid id)
        {
            var item = _applicazionirealiContext.Users.Find(id);
            return item;
        }

        [HttpPost]
        public IActionResult Post(User user)
        {
            _applicazionirealiContext.Add(user);
            _applicazionirealiContext.SaveChanges();
            return Ok("utente_creato");
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, User user)
        {
            if (id != user.Id)
                return BadRequest("id_non_uguale_al_modello");

            if(!UserExist(id))
                return NotFound();

            try
            {
                _applicazionirealiContext.Update(user);
                _applicazionirealiContext.SaveChanges();
                return Ok("utente_aggiornato");
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
            var user = _applicazionirealiContext.Users.Find(id);

            if (user == null)
                return NotFound();

            _applicazionirealiContext.Remove(user);
            _applicazionirealiContext.SaveChanges();
            return Ok("utente_rimosso");
        }

        private bool UserExist(Guid id)
        {
            return _applicazionirealiContext.Users.Any(x => x.Id == id);
        }


        //  ESEMPIO DI API CON VALIDAZIONE DEL MODELLO USANDO LE DATA ANNOTATIONS DEFINITE NELL'ESTENSIONE DEL MODELLO
        //[HttpPost("modelvalidation")]
        //public IActionResult ModelExtWithDataAnnotation(UserExt user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        user.Property1 = "";
        //        user.Method1();

        //        return Ok(user);
        //    }

        //    var errors = ModelState.Select(x => x.Value.Errors)
        //                           .Where(y => y.Count > 0)
        //                           .ToList();

        //    return BadRequest(errors);
        //}
    }
}
