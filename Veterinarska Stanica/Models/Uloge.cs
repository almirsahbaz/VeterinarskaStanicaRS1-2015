using System.ComponentModel;
using VeterinasrkaStanica.Helper;

namespace VeterinarskaStanica.Models
{
    public class Uloge : IEntity
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }
        [DisplayName("Naziv uloge")]
        public string NazivUloge { get; set; }
    }
}