using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTWebApi.Models;
using PTWebApi.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoesController : ControllerBase
    {
        private readonly CondominioDbContext _context = new CondominioDbContext();

        public EstadoesController()
        {
        }

        // GET: api/Estadoes
        [HttpGet("GetAllStatus")]
        public async Task<ActionResult<IEnumerable<Estado>>> GetEstados()
        {
            return await _context.Estados.ToListAsync();
        }

        // GET: api/Estadoes/5
        [HttpGet("GetStatusById/{id}")]
        public async Task<ActionResult<Estado>> GetEstado(int id)
        {
            var estado = await _context.Estados.FindAsync(id);

            if (estado == null)
            {
                return NotFound();
            }

            return estado;
        }

        // PUT: api/Estadoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdateStatus/{id}")]
        public async Task<IActionResult> PutEstado(int id, UpdateStatusViewModel estado)
        {
            if (id != estado.IdEstado)
            {
                return BadRequest();
            }
            var map = new Estado
            {
                IdEstado = estado.IdEstado,
                Desc = estado.Desc
            };
            _context.Entry(map).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoExists(id))
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

        // POST: api/Estadoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("AddStatus")]
        public async Task<ActionResult<Estado>> PostEstado(InsertStatusViewModel estado)
        {
            var map = new Estado
            {
                Desc = estado.Desc
            };
            _context.Estados.Add(map);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstado", new { id = map.IdEstado }, map);
        }

        // DELETE: api/Estadoes/5
        [HttpDelete("RemoveStatus/{id}")]
        public async Task<IActionResult> DeleteEstado(int id)
        {
            var estado = await _context.Estados.FindAsync(id);
            if (estado == null)
            {
                return NotFound();
            }

            _context.Estados.Remove(estado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstadoExists(int id)
        {
            return _context.Estados.Any(e => e.IdEstado == id);
        }
    }
}
