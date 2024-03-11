using System.ComponentModel.DataAnnotations;

namespace ParasIspeak.Models
{
    public class LoginVM
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }

        [Display(Name = "Remember Me")]
        public bool Rememberme { get; set; }
        public string? RedirectUrl { get; set; }
    }
}
