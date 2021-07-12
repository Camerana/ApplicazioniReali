using ApplicazioniReali.Db.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicazioniReali.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicazionirealiContext _applicazionirealiContext;

        public AuthController(ApplicazionirealiContext applicazionirealiContext)
        {
            _applicazionirealiContext = applicazionirealiContext;
        }

        public class LoginForm
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginForm loginForm)
        {
            if(string.IsNullOrEmpty(loginForm.username) || string.IsNullOrEmpty(loginForm.password))
                return BadRequest("Username e/o Password mancanti");

            var user = _applicazionirealiContext.Users.Where(x => x.Username == loginForm.username && x.Password == loginForm.password).FirstOrDefault();

            if (user == null)
                return BadRequest("Username e/o Password errati");

            return Ok(new { Id = user.Id, Username = user.Username });
        }
    }
}
