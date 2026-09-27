using System.Collections.Generic;
using VeterinarskaStanica.Models;

namespace VeterinarskaStanica.Areas.ModulTehnickoOsoblje.Models
{
    public class IzvrsenaUsluga
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public List<Racuni> listaRacuna { get; set; }
        public int RacuniId { get; set; }
        public List<Usluge> listaUsluga { get; set; }

        public int UslugeId { get; set; }

        public List<IzvrseneUsluge> listaIzvrsenihUsluga { get; set; }
        public int IzvrseneUslugeId { get; set; }
    }
}