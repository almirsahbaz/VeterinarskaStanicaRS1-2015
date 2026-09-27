using System;
using System.Collections.Generic;
using VeterinarskaStanica.Models;

namespace VeterinarskaStanica.Areas.ModulTehnickoOsoblje.Models
{
    public class Racun
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public string Naziv { get; set; }
        public DateTime Datum { get; set; }

        public string IznosZaPlatiti { get; set; }

        public List<Racuni> listaRacuna { get; set; }
        public int RacuniId { get; set; }
    }
}