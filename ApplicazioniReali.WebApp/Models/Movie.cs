using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicazioniReali.WebApp.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(5)]
        [Display(Name = "Titolo")]
        public string Title { get; set; }

        [Display(Name = "Data uscita")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Genere")]
        public string Genre { get; set; }

        [Display(Name = "Prezzo")]
        [DataType("decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
