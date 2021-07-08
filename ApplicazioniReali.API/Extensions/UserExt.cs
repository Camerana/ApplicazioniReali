using ApplicazioniReali.Db.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicazioniReali.API.Extensions
{
    // ESTENSIONE DEL MODELLO BASE PER UTILIZZARE DATA ANNOTATIONS, CUSTOM PROPERTIES, CUSTOM METHODS, NON SERIALIZZARE CERTE PROPERTIES, ECC...
    public class UserExt : User
    {
        public string Property1 { get; set; }
        public string Property2 { get; set; }

        [MaxLength(2)]
        public new string Password { get; set; }

        public void Method1()
        {

        }
        public void Method2()
        {

        }
    }
}
