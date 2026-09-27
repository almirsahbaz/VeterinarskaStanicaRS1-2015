using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VeterinarskaStanica.Models;

namespace VeterinarskaStanica.Areas.ModulAdmin.Models
{
    public class AdminDodajVM
    {

        public int Id { get; set; }

        public bool IsDeleted { get; set; }
        [Required(ErrorMessage = "Obavezno polje!")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Obavezno polje!")]
        public string Prezime { get; set; }

        public List<StrucneSpreme> listaSpreme { get; set; }

        public List<KorisnickiNalog> listaNaloga { get; set; }

        public int KorisnickiNalogId { get; set; }

        public List<Gradovi> listaGradova { get; set; }

        public int StrucneSpremeId { get; set; }

        public int GradoviId { get; set; }
    }
}