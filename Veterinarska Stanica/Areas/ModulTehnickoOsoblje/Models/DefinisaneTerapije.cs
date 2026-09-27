using System.Collections.Generic;
using VeterinarskaStanica.Models;

namespace VeterinarskaStanica.Areas.ModulTehnickoOsoblje.Models
{
    public class DefinisaneTerapije
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public int Doziranje { get; set; }

        public List<Pregledi> listaPregleda { get; set; }
        public int PreglediId { get; set; }

        public List<Terapija> listaTerapija { get; set; }

        public int TerapijaId { get; set; }

        public List<DefinsanaTerapija> listaDefinisanihTerapija { get; set; }
        public int DefinisanaTerapijaId { get; set; }
    }
}