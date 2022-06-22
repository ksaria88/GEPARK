using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GEPARK.Data;
using GEPARK.Models;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.Authorization;

namespace GEPARK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarifasController : ControllerBase
    {
        private readonly GEPARKContext _context;

        public TarifasController(GEPARKContext context)
        {
            _context = context;
        }

        // GET: api/Tarifas
        [HttpGet("Tarifas"), Authorize]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Tarifa>>> GetTarifa()
        {
          if (_context.Tarifa == null)
          {
              return NotFound();
          }
            return await _context.Tarifa.ToListAsync();
        }

        // GET: api/Tarifas/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<Tarifa>> GetTarifa(int id)
        {
          if (_context.Tarifa == null)
          {
              return NotFound();
          }
            var tarifa = await _context.Tarifa.FindAsync(id);

            if (tarifa == null)
            {
                return NotFound();
            }

            return tarifa;
        }

        // PUT: api/Tarifas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutTarifa(int id, Tarifa tarifa)
        {
            if (id != tarifa.IdTarifa)
            {
                return BadRequest();
            }

            _context.Entry(tarifa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarifaExists(id))
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

        // POST: api/Tarifas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost, Authorize]
        public async Task<ActionResult<Tarifa>> PostTarifa(Tarifa tarifa)
        {
          if (_context.Tarifa == null)
          {
              return Problem("Entity set 'GEPARKContext.Tarifa'  is null.");
          }
            _context.Tarifa.Add(tarifa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTarifa", new { id = tarifa.IdTarifa }, tarifa);
        }

        // DELETE: api/Tarifas/5
        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeleteTarifa(int id)
        {
            if (_context.Tarifa == null)
            {
                return NotFound();
            }
            var tarifa = await _context.Tarifa.FindAsync(id);
            if (tarifa == null)
            {
                return NotFound();
            }

            _context.Tarifa.Remove(tarifa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TarifaExists(int id)
        {
            return (_context.Tarifa?.Any(e => e.IdTarifa == id)).GetValueOrDefault();
        }
    }
}
