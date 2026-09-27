using System.ComponentModel.DataAnnotations;

namespace VeterinasrkaStanica.Areas.ModulLogin.Models
{
    public class LoginViewModel
    {

        public string Username { get; set; }

        [Required(ErrorMessage = "Unos je obavezan za Password polje!")]
        public string Password { get; set; }

        public string PotvrdaUsername { get; set; }

        public bool Status { get; set; }
    }
}