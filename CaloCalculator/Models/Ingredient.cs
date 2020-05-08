using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaloCalculator.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Ingredient")]
        [Required(ErrorMessage = "This field must not be empty")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Display(Name = "Type")]
        public int? TypeId { get; set; }

        public ICollection<Component> Components { get; set; }
    }
}
