using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicazioniReali.API.Extensions;
using ApplicazioniReali.Db.Data;
using ApplicazioniReali.Db.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
        public IActionResult Test(UserExt user)
        {
            if (ModelState.IsValid)
            {
                user.Property1 = "";
                user.Method1();

                return Ok(user);
            }

            var errors = ModelState.Select(x => x.Value.Errors)
                                   .Where(y => y.Count > 0)
                                   .ToList();

            return BadRequest(errors);
        }

        //PUT       =>      MODIFICA MODEL

        //POST      =>      CREAZIONE MODEL

        //DELETE    =>      CANCELLAZIONE MODEL



    }
}
