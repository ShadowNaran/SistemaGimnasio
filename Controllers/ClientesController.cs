using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GimnasioApi.Data;
using GimnasioApi.Entidades;

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
        //lista de clientes
       [HttpGet]
        public async Task<ActionResult<ICollection<Cliente>>> GetClientes()
        {
            var clientes = await _context.Clientes.ToListAsync();
            return Ok(clientes);
        }
        //busqueda por id
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(Guid id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }
          //agregar un nuevo cliente
       [HttpPost]
        public async Task<ActionResult<Cliente>> CreateCliente([FromBody] Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCliente), new { id = cliente.IdCliente }, cliente);
        }
        // actualizar un cliente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, [FromBody] Cliente cliente)
        {
            if (id != cliente.IdCliente)
                return BadRequest("el id no coincide");

            var existing = await _context.Clientes.FindAsync(id);
            if (existing == null)
                return NotFound();

            
            existing.Nombre = cliente.Nombre;
            existing.CI = cliente.CI;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        // eliminar cliente
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