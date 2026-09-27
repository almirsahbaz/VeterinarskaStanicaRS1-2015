using System.Collections.Generic;
using VeterinarskaStanica.Models;

namespace VeterinarskaStanica.Areas.ModulDoktor.Models
{
    public class Uspostavljena
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public int Intezitet { get; set; }

        public string Komentar { get; set; }

        public List<Dijagnoza> listaDijagnoza { get; set; }
        public int DijagnozaId { get; set; }
        public List<UspostavljenaDijagnoza> listaUspostavljenih { get; set; }
        public int UspostavljenaDijagnozaId { get; set; }
        public List<Pregledi> listaPregleda { get; set; }
        public int PreglediId { get; set; }
    }
}