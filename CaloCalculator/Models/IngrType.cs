using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaloCalculator.Models
{
    public class IngrType
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Type")]
        [Required(ErrorMessage = "This field must not be empty")]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
    }
}
