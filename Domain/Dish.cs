using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Dish
    {
        public Dish() { }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public byte[] Image { get; set; }

        [Required]
        [Display(Name = "Dish Type")]
        public DishType Type { get; set; }

        [Display(Name = "Diet Restriction")]
        public DietRestriction Restriction { get; set; }

        [Required]
        [Display(Name = "Dish Size")]
        public DishSize Size { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        //[Required]
        //public Cook Cook { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
