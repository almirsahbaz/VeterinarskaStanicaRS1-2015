using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VeterinarskaStanica.Models;

namespace VeterinarskaStanica.Areas.ModulDoktor.Models
{
    public class Vrste
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        public string Naziv { get; set; }

        public List<Vrsta> listaVrsta { get; set; }
        public int VrstaId { get; set; }

    }
}