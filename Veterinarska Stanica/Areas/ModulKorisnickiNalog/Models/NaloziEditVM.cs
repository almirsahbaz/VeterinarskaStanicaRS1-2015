using System.ComponentModel.DataAnnotations;

namespace VeterinarskaStanica.Areas.ModulKorisnickiNalog.Models
{
    public class NaloziEditVM
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        [Required(ErrorMessage = "Username je obavezno polje")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password je obavezno polje")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Lozinka mora imati najmanje 4 a najviše 30 karaktera")]
        public string Password { get; set; }

        public bool doc { get; set; }

        public bool teh_osob { get; set; }

        public bool admin { get; set; }
    }
}