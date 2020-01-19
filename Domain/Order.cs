using Domain.Dishsize;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Order
    {
        public Order() { }

        [Key]
        public int Id { get; set; }

        public Client Client { get; set; }

        [Required]
        public List<OrderMeal> Meals { get; set; } = new List<OrderMeal>();

        [Required]
        public double TotalPrice { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; } = DateTime.Now.Date;

    }
}
