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
    public class ClientesController : ControllerBase
    {
        private readonly CondominioDbContext _context = new CondominioDbContext();

        public ClientesController()
        {
        }

        [HttpGet("GetCustomers")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync().ConfigureAwait(false);
        }

        [HttpGet("GetCustomerById/{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        [HttpPut("UpdateCustomer/{id}")]
        public async Task<IActionResult> PutCliente(int id, UpdateCustomerViewModel cliente)
        {
            if (id != cliente.IdCliente)
            {
                return BadRequest();
            }
            var map = new Cliente
            {
                IdCliente = cliente.IdCliente,
                IdEstado = cliente.IdEstado,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Cedula = cliente.Cedula,
                Direccion = cliente.Direccion,
                Telefono = cliente.Telefono
            };
            _context.Entry(map).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        [HttpPost("AddCustomer")]
        public async Task<ActionResult<Cliente>> PostCliente(InsertCustomerViewModel cliente)
        {
            var map = new Cliente
            {
                IdEstado = cliente.IdEstado,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Cedula = cliente.Cedula,
                Direccion = cliente.Direccion,
                Telefono = cliente.Telefono
            };
            _context.Clientes.Add(map);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { id = map.IdCliente }, cliente);
        }

        [HttpDelete("RemoveCustomer/{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.IdCliente == id);
        }
    }
}
