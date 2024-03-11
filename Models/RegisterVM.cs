using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ParasIspeak.Models
{
    public class RegisterVM
    {
        [Required]
        [Display(Name = "Name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        public string confirmPassword { get; set; }

        public string ?redirectedUrl  { get; set; }
        public string ?SelectedRole{ get; set; }

        public IEnumerable<SelectListItem>? RolesList { get; set;}
    }
}
