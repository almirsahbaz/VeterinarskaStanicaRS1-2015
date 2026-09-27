using System;
using System.ComponentModel;
using VeterinasrkaStanica.Helper;

namespace VeterinarskaStanica.Models
{

    public class Uplata : IEntity
    {

        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public string Iznos { get; set; }

        public DateTime Datum { get; set; }

        public Racuni Racuni { get; set; }
        [DisplayName("Raèun")]
        public int RacuniId { get; set; }

    }
}
