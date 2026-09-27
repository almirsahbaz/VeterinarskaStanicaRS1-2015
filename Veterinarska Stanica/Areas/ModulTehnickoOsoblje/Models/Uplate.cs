using System;
using System.Collections.Generic;
using VeterinarskaStanica.Models;

namespace VeterinarskaStanica.Areas.ModulTehnickoOsoblje.Models
{
    public class Uplate
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public string Iznos { get; set; }

        public DateTime Datum { get; set; }

        public List<Racuni> listaRacuna { get; set; }

        public int RacuniId { get; set; }

        public List<Uplata> listaUplata { get; set; }
        public int UplataId { get; set; }


    }
}