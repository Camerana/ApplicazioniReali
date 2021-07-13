using ApplicazioniReali.API.ModelsIdentity;
using ApplicazioniReali.Db.Data;
using ApplicazioniReali.Db.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicazioniReali.Db.Seeds
{
    public class SeedDB
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var _context = serviceProvider.GetRequiredService<ApplicazionirealiContext>();
            var _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            if (!_context.Database.CanConnect() || _context.Database.GetPendingMigrations().Any())
                _context.Database.Migrate();

            if (!_context.Users.Any())
            {
                AppUser appUser = new AppUser()
                {
                    Email = "test@gmail.com",
                    UserName = "test",
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = _userManager.CreateAsync(appUser, "Test@123").Result;

                if (result.Succeeded)
                {
                    User user = new User()
                    {
                        FirstName = "Alessio",
                        LastName = "Filippucci",
                        Dob = new DateTime(1989, 06, 11),
                        AspNetUsersId = appUser.Id
                    };
                    _context.Add(user);
                    _context.SaveChanges();
                }
            }
        }
    }
}
