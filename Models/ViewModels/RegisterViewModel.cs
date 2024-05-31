using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AMVTravels.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        [Compare("Password",ErrorMessage = "Las Contraseñas no coinciden")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Area/Sector")]
        public string BusinessArea { get; set; }  
    }
}
