using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Client : AbstractUser
    {
        public Client() { }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        
        [Required]
        public string City { get; set; }
        
        [Required]
        public string Street { get; set; }
        
        [Required]
        public int HouseNumber { get; set; }
      
        [Required]
        public string Addition { get; set; }
        
        [Required]
        public string PostalCode { get; set; }
    }
}
