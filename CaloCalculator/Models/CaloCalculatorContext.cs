using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CaloCalculator.Models
{
    public class CaloCalculatorContext : DbContext
    {
        public virtual DbSet<IngrType> IngrTypes { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Dish> Dishes { get; set; }
        public virtual DbSet<Component> Components { get; set; }
        public virtual DbSet<Record> Records { get; set; }

        public CaloCalculatorContext(DbContextOptions<CaloCalculatorContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
