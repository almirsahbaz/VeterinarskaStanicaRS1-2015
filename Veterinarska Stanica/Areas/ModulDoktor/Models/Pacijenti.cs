using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VeterinarskaStanica.Models;

namespace VeterinarskaStanica.Areas.ModulDoktor.Models
{
    public class Pacijenti
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }
        [Required(ErrorMessage = "Obavezno polje!")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Obavezno polje!")]

        public DateTime Godiste { get; set; }

        public DateTime DatumPrijema { get; set; }

        public List<Vlasnik> listaVlasnika { get; set; }

        public List<Vrsta> listaVrsta { get; set; }

        public int VlasnikId { get; set; }

        public int VrstaId { get; set; }

        public List<Pacijent> listaPacijenata { get; set; }
        public int PacijentId { get; set; }

        public List<KorisnickiNalog> listaNaloga { get; set; }

        public int KorisnickiNalogId { get; set; }

    }
}