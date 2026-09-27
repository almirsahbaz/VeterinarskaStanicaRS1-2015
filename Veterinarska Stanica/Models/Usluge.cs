using System;
using System.ComponentModel;
using VeterinasrkaStanica.Helper;

namespace VeterinarskaStanica.Models
{
    public class Usluge : IEntity
    {

        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public string Naziv { get; set; }
        [DisplayName("Datum usluge")]
        public DateTime DatumUsluge { get; set; }


    }
}
