using System.Collections.Generic;
using VeterinarskaStanica.Models;

namespace VeterinarskaStanica.Areas.ModulTehnickoOsoblje.Models
{
    public class KupljeniLijek
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public int Kolicina { get; set; }

        public List<Racuni> listaRacuna { get; set; }
        public int RacuniId { get; set; }

        public List<Lijekovi> listaLijekova { get; set; }

        public int LijekoviId { get; set; }

        public List<KupljeniLijekovi> listaKupljenihLijekova { get; set; }

        public int KupljeniLijekoviId { get; set; }

    }
}