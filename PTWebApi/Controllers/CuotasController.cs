using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTWebApi.Models;
using PTWebApi.ViewModels;

namespace PTWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuotasController : ControllerBase
    {
        private readonly CondominioDbContext _context = new CondominioDbContext();

        public CuotasController()
        {
        }
        [HttpGet("GetAllFees")]
        public async Task<ActionResult<IEnumerable<Cuota>>> GetCuotas()
        {
            return await _context.Cuotas.ToListAsync();
        }

        [HttpGet("GetFeeById/{id}")]
        public async Task<ActionResult<Cuota>> GetCuota(int id)
        {
            var cuota = await _context.Cuotas.FindAsync(id);

            if (cuota == null)
            {
                return NotFound();
            }

            return cuota;
        }

        [HttpPut("UpdateFee/{id}")]
        public async Task<IActionResult> PutCuota(int id, UpdateFeeViewModel cuota)
        {
            if (id != cuota.IdCuota)
            {
                return BadRequest();
            }
            var map = new Cuota
            {
                IdCuota = cuota.IdCuota,
                IdEstado = cuota.IdEstado,
                Monto = cuota.Monto,
                Cantidad = cuota.Cantidad,
                Fecha = cuota.Fecha,
                Mora = cuota.Mora,
                MontoTotal = cuota.MontoTotal,
                IdPago = cuota.IdPago,

            };
            _context.Entry(map).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuotaExists(id))
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

        // POST: api/Cuotas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("AddFee")]
        public async Task<ActionResult<Cuota>> PostCuota(InsertFeeViewModel cuota)
        {
            var pago = _context.Pagos.FirstOrDefault(x => x.IdPago == cuota.IdPago);
            if(cuota.Fecha > pago.FechaVencimiento)
            {
            cuota.Mora = cuota.Monto * 0.05;
            }
            else
            {
                cuota.Mora = 0;
            }
            
            cuota.MontoTotal = cuota.Monto + cuota.Mora;
            var map = new Cuota
            {
                IdEstado = cuota.IdEstado,
                Monto = cuota.Monto,
                Cantidad = cuota.Cantidad,
                Fecha = cuota.Fecha,
                Mora = cuota.Mora,
                MontoTotal = cuota.MontoTotal,
                IdPago = cuota.IdPago,

            };
            _context.Cuotas.Add(map);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCuota", new { id = map.IdCuota }, map);
        }

        // DELETE: api/Cuotas/5
        [HttpDelete("RemoveFee/{id}")]
        public async Task<IActionResult> DeleteCuota(int id)
        {
            var cuota = await _context.Cuotas.FindAsync(id);
            if (cuota == null)
            {
                return NotFound();
            }

            _context.Cuotas.Remove(cuota);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CuotaExists(int id)
        {
            return _context.Cuotas.Any(e => e.IdCuota == id);
        }
    }
}
