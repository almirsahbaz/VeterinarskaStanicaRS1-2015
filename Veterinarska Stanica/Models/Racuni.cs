using System;
using System.ComponentModel;
using VeterinasrkaStanica.Helper;

namespace VeterinarskaStanica.Models
{

    public class Racuni : IEntity
    {

        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public string Naziv { get; set; }
        public DateTime Datum { get; set; }
        [DisplayName("Iznos za platiti")]
        public string IznosZaPlatiti { get; set; }

    }
}
