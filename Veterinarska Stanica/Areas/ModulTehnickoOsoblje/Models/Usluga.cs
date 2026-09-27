using System;
using System.Collections.Generic;
using VeterinarskaStanica.Models;

namespace VeterinarskaStanica.Areas.ModulTehnickoOsoblje.Models
{
    public class Usluga
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public string Naziv { get; set; }

        public DateTime DatumUsluge { get; set; }

        public List<Usluge> listaUsluga { get; set; }
        public int UslugeId { get; set; }

    }
}