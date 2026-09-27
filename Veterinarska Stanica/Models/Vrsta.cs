using VeterinasrkaStanica.Helper;
namespace VeterinarskaStanica.Models
{

    public class Vrsta : IEntity
    {

        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public string Naziv { get; set; }


    }
}
