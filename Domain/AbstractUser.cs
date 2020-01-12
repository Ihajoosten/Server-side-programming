using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class AbstractUser : IdentityUser
    {
        [Required(ErrorMessage = "Your firstname is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Your last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Your email is required")]
        public override string Email { get; set; }

        [Required(ErrorMessage = "Your password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confrimation password is required")]
        [Compare("Password", ErrorMessage = "The password does not match")]
        public string ConfirmPassword { get; set; }
    }
}
