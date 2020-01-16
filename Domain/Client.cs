using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Client
    {
        public Client() { }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Your firstname is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Your last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Your email is required")]
        public string Email { get; set; }

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
