using System.Collections.Generic;
using VeterinarskaStanica.Models;

namespace VeterinarskaStanica.Areas.ModulDoktor.Models
{
    public class Dijagnoze
    {

        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public string Naziv_dijagnoze { get; set; }

        public string Sifra_dijagnoze { get; set; }
        public List<Dijagnoza> listaDijagnoza { get; set; }
        public int DijagnozaId { get; set; }
    }
}