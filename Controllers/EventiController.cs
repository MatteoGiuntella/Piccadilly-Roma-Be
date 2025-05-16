using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Piccadilly_Roma_Be.Data;
using Piccadilly_Roma_Be.Models;

namespace Piccadilly_Roma_Be.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventiController : ControllerBase
    {
        private readonly RistoranteContext _context;

        public EventiController(RistoranteContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evento>>> GetEventi()
        {
            return await _context.Eventi.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> GetEvento(int id)
        {
            var evento = await _context.Eventi.FindAsync(id);
            if (evento == null)
                return NotFound();

            return evento;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Evento>> PostEvento(Evento evento)
        {
            _context.Eventi.Add(evento);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEvento), new { id = evento.Id }, evento);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvento(int id, Evento evento)
        {
            if (id != evento.Id) return BadRequest();

            _context.Entry(evento).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            var evento = await _context.Eventi.FindAsync(id);
            if (evento == null) return NotFound();

            _context.Eventi.Remove(evento);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
