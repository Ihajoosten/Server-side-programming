using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Client : AbstractUser
    {
        public Client() { }

        [Required(ErrorMessage = "Your birthday is required")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Your city code is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Your street code is required")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Your housenumber is required")]
        public int HouseNumber { get; set; }
      
        public string Addition { get; set; }
        
        [Required(ErrorMessage = "Your postal code is required")]
        public string PostalCode { get; set; }

        public bool Salt { get; set; }

        public bool Diabetes { get; set; }

        public bool Gluten { get; set; }
    }
}
