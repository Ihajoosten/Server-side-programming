using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public abstract class AbstractUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string PhoneNumber { get; set; }
        
        [Required]
        public string Password { get; set; }

    }
}
