using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models.Account
{
    public class UpdateViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public int HouseNumber { get; set; }

        public string Addition { get; set; }

        [Required]
        public string PostalCode { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public bool Gluten { get; set; }

        public bool Salt { get; set; }

        public bool Diabetes { get; set; }
    }
}
