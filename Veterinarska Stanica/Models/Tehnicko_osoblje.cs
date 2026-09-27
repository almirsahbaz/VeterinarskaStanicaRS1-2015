using VeterinasrkaStanica.Helper;

namespace VeterinarskaStanica.Models
{
    public class Tehnicko_osoblje : IEntity
    {

        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public string Vrsta_posla { get; set; }

        public string Ime { get; set; }

        public string Prezime { get; set; }


        public virtual Gradovi Gradovi { get; set; }

        public virtual KorisnickiNalog KorisnickiNalog { get; set; }

        public virtual Odjeli Odjeli { get; set; }

        public int? GradoviId { get; set; }

        public int? OdjeliId { get; set; }

        public int KorisnickiNalogId { get; set; }


    }
}
