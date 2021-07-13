using ApplicazioniReali.API.Models.Request;
using ApplicazioniReali.API.ModelsIdentity;
using ApplicazioniReali.Db.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly ApplicazionirealiContext _context;
        private UserManager<AppUser> _userManager;

        public AuthController(ApplicazionirealiContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if(string.IsNullOrEmpty(loginModel.Username) || string.IsNullOrEmpty(loginModel.Password))
                return BadRequest("Username e/o Password mancanti");

            var appUser = await _userManager.FindByNameAsync(loginModel.Username);

            if (appUser == null || !await _userManager.CheckPasswordAsync(appUser, loginModel.Password))
                return BadRequest("Username e/o Password errati");

            var user = await _context.Users.Where(x => x.AspNetUsersId == appUser.Id).FirstOrDefaultAsync();

            if(user == null)
                return NotFound("User not found or deleted");

            return Ok(new { user.Id, IdentityId = appUser.Id, appUser.Email, user.FirstName, user.LastName });
        }
    }
}
