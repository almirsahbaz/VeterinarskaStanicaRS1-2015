using System.Collections.Generic;
using VeterinarskaStanica.Models;

namespace VeterinarskaStanica.Areas.ModulTehnickoOsoblje.Models
{
    public class Uloga
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public string NazivUloge { get; set; }

        public List<Uloge> listaUloga { get; set; }
        public int UlogeId { get; set; }


    }
}