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
    public class ResidencialesController : ControllerBase
    {
        private readonly CondominioDbContext _context = new CondominioDbContext();

        public ResidencialesController()
        {
        }

        // GET: api/Residenciales
        [HttpGet("GetAllResidentials")]
        public async Task<ActionResult<IEnumerable<Residencial>>> GetResidenciales()
        {
            return await _context.Residenciales.ToListAsync();
        }

        // GET: api/Residenciales/5
        [HttpGet("GetResidentialById/{id}")]
        public async Task<ActionResult<Residencial>> GetResidencial(int id)
        {
            var residencial = await _context.Residenciales.FindAsync(id);

            if (residencial == null)
            {
                return NotFound();
            }

            return residencial;
        }

        // PUT: api/Residenciales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdateResidential/{id}")]
        public async Task<IActionResult> PutResidencial(int id, UpdateResidentialViewModel residencial)
        {
            if (id != residencial.IdResidencial)
            {
                return BadRequest();
            }
            var map = new Residencial
            {
                IdResidencial = residencial.IdResidencial,
                Nombre = residencial.Nombre,
                Direccion = residencial.Direccion,
                Precio = residencial.Precio,
                Descripcion = residencial.Descripcion,
                IdEstado = residencial.IdEstado
            };
            _context.Entry(map).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResidencialExists(id))
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

        // POST: api/Residenciales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("AddResidential")]
        public async Task<ActionResult<Residencial>> PostResidencial(InsertResidentialViewModel residencial)
        {
            var map = new Residencial
            {
                Nombre = residencial.Nombre,
                Direccion = residencial.Direccion,
                Precio = residencial.Precio,
                Descripcion = residencial.Descripcion,
                IdEstado = residencial.IdEstado
            };
            _context.Residenciales.Add(map);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResidencial", new { id = map.IdResidencial }, map);
        }

        // DELETE: api/Residenciales/5
        [HttpDelete("RemoveResidential/{id}")]
        public async Task<IActionResult> DeleteResidencial(int id)
        {
            var residencial = await _context.Residenciales.FindAsync(id);
            if (residencial == null)
            {
                return NotFound();
            }

            _context.Residenciales.Remove(residencial);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ResidencialExists(int id)
        {
            return _context.Residenciales.Any(e => e.IdResidencial == id);
        }
    }
}
