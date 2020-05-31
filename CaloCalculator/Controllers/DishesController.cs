using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CaloCalculator.Models;
using CaloCalculator.ViewModels;

namespace CaloCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private readonly CaloCalculatorContext _context;

        public DishesController(CaloCalculatorContext context)
        {
            _context = context;
        }

        // GET: api/Dishes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dish>>> GetDishes()
        {
            return await _context.Dishes.ToListAsync();
        }

        // GET: api/Dishes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dish>> GetDish(int id)
        {
            var dish = await _context.Dishes.FindAsync(id);

            if (dish == null)
            {
                return NotFound();
            }

            return dish;
        }

        // PUT: api/Dishes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id:int}/{Name:alpha}")]
        public async Task<IActionResult> PutDish([FromRoute] int id, [FromRoute] string Name, [FromBody] IEnumerable<ComponentViewModel> componentVMs)
        {
            var dish = _context.Dishes.Find(id);

            if (dish == null)
            {
                return NotFound();
            }

            dish.Name = Name;

            var oldComponents = _context.Components.Where(c => c.DishId == id).ToList();
            _context.Components.RemoveRange(oldComponents);

            foreach (var comp in componentVMs)
            {
                if (!(_context.Ingredients.Any(i => i.Id == comp.IngredientId))) return BadRequest();
                Component component = new Component()
                {
                    DishId = dish.Id,
                    IngredientId = comp.IngredientId,
                    Grams = comp.Grams
                };
                _context.Components.Add(component);
            }
            await _context.SaveChangesAsync();

            var relatedComponents = _context.Components.Where(c => c.DishId == dish.Id);
            int totalWeight = 0;
            int totalKcals = 0;
            foreach (var comp in relatedComponents)
            {
                int kcalsPer100g = _context.Ingredients.Find(comp.IngredientId).KcalsPer100g;
                int kcals = comp.Grams * kcalsPer100g / 100;
                totalKcals += kcals;
                totalWeight += comp.Grams;
            }
            dish.KcalsPer100g = 100 * totalKcals / totalWeight;
            _context.Dishes.Update(dish);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DishExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Dishes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Route("{Name:alpha}")]
        public async Task<ActionResult<Dish>> PostDish([FromRoute] string Name, [FromBody] IEnumerable<ComponentViewModel> componentVMs)
        {
            var dish = new Dish() { Name = Name };
            _context.Dishes.Add(dish);
            await _context.SaveChangesAsync();

            foreach(var comp in componentVMs)
            {
                if (!(_context.Ingredients.Any(i => i.Id == comp.IngredientId))) return BadRequest();
                Component component = new Component()
                {
                    DishId = dish.Id,
                    IngredientId = comp.IngredientId,
                    Grams = comp.Grams
                };
                _context.Components.Add(component);
            }
            await _context.SaveChangesAsync();

            var relatedComponents = _context.Components.Where(c => c.DishId == dish.Id);
            int totalWeight = 0;
            int totalKcals = 0;
            foreach(var comp in relatedComponents)
            {
                int kcalsPer100g = _context.Ingredients.Find(comp.IngredientId).KcalsPer100g;
                int kcals = comp.Grams * kcalsPer100g / 100;
                totalKcals += kcals;
                totalWeight += comp.Grams;
            }
            dish.KcalsPer100g = 100 * totalKcals / totalWeight;
            _context.Dishes.Update(dish);

            _context.SaveChanges();

            return CreatedAtAction("GetDish", new { id = dish.Id }, dish);
        }

        // DELETE: api/Dishes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Dish>> DeleteDish(int id)
        {
            var dish = await _context.Dishes.FindAsync(id);

            if (dish == null)
            {
                return NotFound();
            }

            _context.Dishes.Remove(dish);
            await _context.SaveChangesAsync();

            return dish;
        }

        private bool DishExists(int id)
        {
            return _context.Dishes.Any(e => e.Id == id);
        }
    }
}
