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
    public class TicketPerdidosController : ControllerBase
    {
        private readonly GEPARKContext _context;

        public TicketPerdidosController(GEPARKContext context)
        {
            _context = context;
        }

        // GET: api/TicketPerdidos
        [HttpGet, Authorize]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<TicketPerdido>>> GetTicketPerdido()
        {
          if (_context.TicketPerdido == null)
          {
              return NotFound();
          }
            return await _context.TicketPerdido.ToListAsync();
        }

        // GET: api/TicketPerdidos/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<TicketPerdido>> GetTicketPerdido(int id)
        {
          if (_context.TicketPerdido == null)
          {
              return NotFound();
          }
            var ticketPerdido = await _context.TicketPerdido.FindAsync(id);

            if (ticketPerdido == null)
            {
                return NotFound();
            }

            return ticketPerdido;
        }

        // PUT: api/TicketPerdidos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutTicketPerdido(int id, TicketPerdido ticketPerdido)
        {
            if (id != ticketPerdido.IdTicketPerdido)
            {
                return BadRequest();
            }

            _context.Entry(ticketPerdido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketPerdidoExists(id))
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

        // POST: api/TicketPerdidos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost, Authorize]
        public async Task<ActionResult<TicketPerdido>> PostTicketPerdido(TicketPerdido ticketPerdido)
        {
          if (_context.TicketPerdido == null)
          {
              return Problem("Entity set 'GEPARKContext.TicketPerdido'  is null.");
          }
            _context.TicketPerdido.Add(ticketPerdido);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicketPerdido", new { id = ticketPerdido.IdTicketPerdido }, ticketPerdido);
        }

        // DELETE: api/TicketPerdidos/5
        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeleteTicketPerdido(int id)
        {
            if (_context.TicketPerdido == null)
            {
                return NotFound();
            }
            var ticketPerdido = await _context.TicketPerdido.FindAsync(id);
            if (ticketPerdido == null)
            {
                return NotFound();
            }

            _context.TicketPerdido.Remove(ticketPerdido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketPerdidoExists(int id)
        {
            return (_context.TicketPerdido?.Any(e => e.IdTicketPerdido == id)).GetValueOrDefault();
        }
    }
}
