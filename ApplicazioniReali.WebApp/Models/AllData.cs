using ApplicazioniReali.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicazioniReali.WebApp.Models
{
    public class AllData
    {
        public List<User> Users { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
