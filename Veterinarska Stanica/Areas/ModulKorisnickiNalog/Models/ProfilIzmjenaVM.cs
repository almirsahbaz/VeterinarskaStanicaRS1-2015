using System.ComponentModel.DataAnnotations;

namespace VeterinarskaStanica.Areas.ModulKorisnickiNalog.Models
{
    public class ProfilIzmjenaVM
    {
        [Required(ErrorMessage = "Obavezno polje!")]
        public string StariPassword { get; set; }

        [Required(ErrorMessage = "Obavezno polje!")]
        public string NoviPassword { get; set; }
    }
}