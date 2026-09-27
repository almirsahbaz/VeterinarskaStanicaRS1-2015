using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VeterinarskaStanica.Models;

namespace VeterinarskaStanica.Areas.ModulAdmin.Models
{
    public class DoktorDodajVM
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        [Required(ErrorMessage = "Obavezno unijeti")]
        public string Pozicija { get; set; }

        [Required(ErrorMessage = "Obavezno unijeti")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Obavezno unijeti")]
        public string Prezime { get; set; }

        public List<Gradovi> listaGradova { get; set; }

        public List<KorisnickiNalog> listaNaloga { get; set; }

        public List<Zvanje> listaZavnja { get; set; }

        public int GradoviId { get; set; }

        public int KorisnickiNalogId { get; set; }

        public int ZvanjeId { get; set; }
    }

}