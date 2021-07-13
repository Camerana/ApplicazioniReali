using ApplicazioniReali.API.Helpers;
using ApplicazioniReali.API.Models.Request;
using ApplicazioniReali.API.ModelsIdentity;
using ApplicazioniReali.Db.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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
            if (string.IsNullOrEmpty(loginModel.Username) || string.IsNullOrEmpty(loginModel.Password))
                return Unauthorized();

            var appUser = await _userManager.FindByNameAsync(loginModel.Username);

            if (appUser == null || !await _userManager.CheckPasswordAsync(appUser, loginModel.Password))
                return Unauthorized();

            var user = await _context.Users.Where(x => x.AspNetUsersId == appUser.Id).FirstOrDefaultAsync();

            if (user == null)
                return Unauthorized();

            var authClaims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, appUser.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("full_name", $"{user.FirstName} {user.LastName}")
                };

            var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Common.GlobalSettings.SecretKey));

            var token = new JwtSecurityToken(
                issuer: "https://localhost:5003",
                audience: "https://localhost:5003",
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddSeconds(10),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
            });
        }
    }
}
