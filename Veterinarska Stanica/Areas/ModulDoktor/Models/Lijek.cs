using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VeterinarskaStanica.Models;

namespace VeterinarskaStanica.Areas.ModulDoktor.Models
{
    public class Lijek
    {

        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        [Required(ErrorMessage = "Obavezno polje!")]

        public string Naziv { get; set; }

        public string Vrsta { get; set; }

        public DateTime Rok_trajanja { get; set; }

        public List<Lijekovi> listaLijekova { get; set; }
        public int LijekId { get; set; }
    }
}