using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicazioniReali.Db.Data;
using ApplicazioniReali.Db.Models;
using Microsoft.AspNetCore.Mvc;

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

    }
}
