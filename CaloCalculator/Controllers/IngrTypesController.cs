using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CaloCalculator.Models;

namespace CaloCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngrTypesController : ControllerBase
    {
        private readonly CaloCalculatorContext _context;

        public IngrTypesController(CaloCalculatorContext context)
        {
            _context = context;
        }

        // GET: api/IngrTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngrType>>> GetIngrTypes()
        {
            return await _context.IngrTypes.ToListAsync();
        }

        // GET: api/IngrTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IngrType>> GetIngrType(int id)
        {
            var ingrType = await _context.IngrTypes.FindAsync(id);

            if (ingrType == null)
            {
                return NotFound();
            }

            return ingrType;
        }

        // PUT: api/IngrTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngrType(int id, IngrType ingrType)
        {
            if (id != ingrType.Id)
            {
                return BadRequest();
            }

            _context.Entry(ingrType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngrTypeExists(id))
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

        // POST: api/IngrTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<IngrType>> PostIngrType(IngrType ingrType)
        {
            _context.IngrTypes.Add(ingrType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIngrType", new { id = ingrType.Id }, ingrType);
        }

        // DELETE: api/IngrTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IngrType>> DeleteIngrType(int id)
        {
            var ingrType = await _context.IngrTypes.FindAsync(id);
            if (ingrType == null)
            {
                return NotFound();
            }

            _context.IngrTypes.Remove(ingrType);
            await _context.SaveChangesAsync();

            return ingrType;
        }

        private bool IngrTypeExists(int id)
        {
            return _context.IngrTypes.Any(e => e.Id == id);
        }
    }
}
