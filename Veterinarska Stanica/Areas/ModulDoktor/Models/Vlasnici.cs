using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VeterinarskaStanica.Models;

namespace VeterinarskaStanica.Areas.ModulDoktor.Models
{
    public class Vlasnici
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }
        [Required(ErrorMessage = "Obavezno polje")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        public string Prezime { get; set; }

        public List<Vlasnik> listaVlasnika { get; set; }
        public int VlasnikId { get; set; }


    }
}