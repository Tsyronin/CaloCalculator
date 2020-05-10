using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaloCalculator.Models
{
    public class Dish
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Dish")]
        [Required(ErrorMessage = "This field must not be empty")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Display(Name = "Kcals per 100g")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int? KcalsPer100g { get; set; }

        public ICollection<Component> Components { get; set; }

        public ICollection<Record> Records { get; set; }

    }
}
