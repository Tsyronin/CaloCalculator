using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaloCalculator.Models
{
    public class Record
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Dish")]
        [Required(ErrorMessage = "This field must not be empty")]
        public int DishId { get; set; }

        [Display(Name = "Grams")]
        [Required(ErrorMessage = "This field must not be empty")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Grams { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "This field must not be empty")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
