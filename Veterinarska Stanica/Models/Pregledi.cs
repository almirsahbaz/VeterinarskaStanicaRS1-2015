using System;
using System.Collections.Generic;
using System.ComponentModel;
using VeterinasrkaStanica.Helper;

namespace VeterinarskaStanica.Models
{

    public class Pregledi : IEntity
    {
        internal List<Pacijent> listaPacijenata;
        internal List<Doktor> listaDoktora;
        internal List<Racuni> listaRacuna;

        public int Id { get; set; }

        public bool IsDeleted { get; set; }
        [DisplayName("Datum pregleda")]
        public DateTime DatumPregleda { get; set; }

        public Doktor Doktori { get; set; }
        [DisplayName("Doktor")]
        public int DoktorId { get; set; }

        public Pacijent Pacijent { get; set; }
        [DisplayName("Pacijent")]
        public int PacijentId { get; set; }

        public Racuni Racuni { get; set; }
        [DisplayName("Raèun")]
        public int RacuniId { get; set; }


    }
}
