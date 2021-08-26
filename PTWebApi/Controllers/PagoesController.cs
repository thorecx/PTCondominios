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
    public class PagoesController : ControllerBase
    {
        private readonly CondominioDbContext _context = new CondominioDbContext();

        public PagoesController()
        {
        }

        // GET: api/Pagoes
        [HttpGet("GetAllPayments")]
        public async Task<ActionResult<IEnumerable<Pago>>> GetPagos()
        {
            return await _context.Pagos.ToListAsync();
        }

        // GET: api/Pagoes/5
        [HttpGet("GetPaymentById{id}")]
        public async Task<ActionResult<Pago>> GetPago(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);

            if (pago == null)
            {
                return NotFound();
            }

            return pago;
        }

        // PUT: api/Pagoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdatePayment/{id}")]
        public async Task<IActionResult> PutPago(int id, UpdatePaymentViewModel pago)
        {
            if (id != pago.IdPago)
            {
                return BadRequest();
            }
            var map = new Pago
            {
                Monto = pago.Monto,
                FechaVencimiento = pago.FechaVencimiento,
                IdCliente = pago.IdCliente,
                IdEstado = pago.IdEstado
            };
            _context.Entry(map).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagoExists(id))
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

        // POST: api/Pagoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("AddPayment")]
        public async Task<ActionResult<Pago>> PostPago(InsertPaymentViewModel pago)
        {
            var map = new Pago
            {
                Monto = pago.Monto,
                FechaVencimiento = pago.FechaVencimiento,
                IdCliente = pago.IdCliente,
                IdEstado = pago.IdEstado
            };
            _context.Pagos.Add(map);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPago", new { id = map.IdPago }, map);
        }

        // DELETE: api/Pagoes/5
        [HttpDelete("RemovePayment/{id}")]
        public async Task<IActionResult> DeletePago(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null)
            {
                return NotFound();
            }

            _context.Pagos.Remove(pago);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PagoExists(int id)
        {
            return _context.Pagos.Any(e => e.IdPago == id);
        }
    }
}
