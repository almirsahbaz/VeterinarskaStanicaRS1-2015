using System.ComponentModel;
using VeterinasrkaStanica.Helper;

namespace VeterinarskaStanica.Models
{
    public class KupljeniLijekovi : IEntity
    {

        public int Id { get; set; }

        public bool IsDeleted { get; set; }
        [DisplayName("Kolièina")]
        public int Kolicina { get; set; }

        public Racuni Racuni { get; set; }
        [DisplayName("Raèun")]
        public int RacuniId { get; set; }

        public Lijekovi Lijekovi { get; set; }
        [DisplayName("Lijek")]
        public int LijekoviId { get; set; }



    }
}
