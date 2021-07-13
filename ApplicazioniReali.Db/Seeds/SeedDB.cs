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
                    Email = "alessio.filippucci@camerana.it",
                    UserName = "alessio.filippucci",
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = _userManager.CreateAsync(appUser, "Passw0rd!").Result;

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
            
            if (!_context.Movies.Any())
            {
                var movies = new List<Movie>()
                {
                    new Movie()
                    {
                        Title = "Pulp Fiction",
                        ReleaseDate = new DateTime(1994, 01, 01),
                        Genre = "Giallo/Commedia",
                        Price = 10.99m
                    },
                    new Movie()
                    {
                        Title = "Taxi Driver",
                        ReleaseDate = new DateTime(1976, 01, 01),
                        Genre = "Giallo/Drammatico",
                        Price = 6.99m
                    },
                    new Movie()
                    {
                        Title = "Bastardi senza gloria",
                        ReleaseDate = new DateTime(2009, 01, 01),
                        Genre = "Guerra/Azione",
                        Price = 15.99m
                    }
                };

                _context.AddRange(movies);
                _context.SaveChanges();
            }
        }
    }
}
