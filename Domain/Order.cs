﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Order
    {
        public Order() { }

        [Key]
        public int Id { get; set; }

        [Required]
        public Client Client { get; set; }

        [Required]
        public List<Meal> OrderMeals { get; set; } = new List<Meal>();

        //[Required]
        //public Dish Dish { get; set; }

        //[Required]
        //[Display(Name = "Dish Size")]
        //public DishSize Size { get; set; }

        //[Required]
        //[DataType(DataType.Date)]
        //public DateTime OrderDate { get; set; }
    }
}
