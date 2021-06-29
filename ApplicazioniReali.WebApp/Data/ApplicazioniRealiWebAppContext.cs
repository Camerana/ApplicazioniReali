using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApplicazioniReali.WebApp.Models;

namespace ApplicazioniReali.WebApp.Data
{
    public class ApplicazioniRealiWebAppContext : DbContext
    {
        public ApplicazioniRealiWebAppContext (DbContextOptions<ApplicazioniRealiWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicazioniReali.WebApp.Models.Movie> Movie { get; set; }
    }
}
