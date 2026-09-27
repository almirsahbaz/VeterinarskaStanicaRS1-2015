using System.Collections.Generic;
using VeterinarskaStanica.Models;

namespace VeterinarskaStanica.Areas.ModulAdmin.Models
{
    public class DeleteVM
    {
        public List<Administrator> listaAdministratora { get; set; }
        public int AdministratorId { get; set; }

        public bool AktivacijaAdmina { get; set; }

        public List<Doktor> listaDoktora { get; set; }
        public int DoktorId { get; set; }
        public bool AktivacijaDoktro { get; set; }

        public List<Tehnicko_osoblje> listaTehnicara { get; set; }
        public int Tehnicko_osobljeId { get; set; }
        public bool AktivacijaTehnicar { get; set; }
    }
}