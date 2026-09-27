using System.Collections.Generic;
using VeterinarskaStanica.Models;

namespace VeterinarskaStanica.Areas.ModulKorisnickiNalog.Models
{
    public class NalogDeleteVM
    {
        public List<KorisnickiNalog> listaNaloga { get; set; }
        public int KorisnickiNalogId { get; set; }

        public bool Aktivacija { get; set; }
    }
}