using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GimnasioApi.Data;
using GimnasioApi.Entidades;
using GimnasioApi.DTOs; 

namespace GimnasioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        // 1. LISTA DE CLIENTES USANDO DTO
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetClientes()
        {
            var clientes = await _context.Clientes
                .Include(c => c.Telefonos)
                .Select(c => new ClienteDTO
                {
                    IdCliente = c.IdCliente,
                    Nombre = c.Nombre,
                    CI = c.CI,
                    // Convertimos la coleccion de objetos Telefono a una lista de strings
                    NumerosTelefonicos = c.Telefonos.Select(t => t.Numero).ToList()
                })
                .ToListAsync();

            return Ok(clientes);
        }

        // 2. BUSQUEDA POR ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Telefonos)
                .FirstOrDefaultAsync(c => c.IdCliente == id);

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        // 3. AGREGAR NUEVO CLIENTE
        [HttpPost]
        public async Task<ActionResult<Cliente>> CreateCliente([FromBody] Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCliente), new { id = cliente.IdCliente }, cliente);
        }

        // 4. ACTUALIZAR CLIENTE
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, [FromBody] Cliente cliente)
        {
            var existing = await _context.Clientes.FindAsync(id);
            if (existing == null)
                return NotFound("El ID no se encuentra");

            existing.Nombre = cliente.Nombre;
            existing.CI = cliente.CI;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 5. ELIMINAR CLIENTE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return NotFound();

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}