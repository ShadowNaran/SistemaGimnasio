using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GimnasioApi.Data;
using GimnasioApi.Entidades;
using GimnasioApi.DTO.Cliente.AgregarCliente;

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

        //  LISTA DE CLIENTES USANDO DTO
       [HttpGet]
           public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
          {
             return await _context.Clientes.ToListAsync(); 
          } 

        //  BUSQUEDA POR ID
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
          //agregar un nuevo cliente
       [HttpPost]
        public async Task<ActionResult<AgregarClienteOutput>> CreateCliente([FromBody] AgregarClienteOutput cliente)
        {

         var entrada = new Cliente
         {
            Nombre = cliente.Nombre,
            CI = cliente.CI
        };

        _context.Clientes.Add(entrada);
        await _context.SaveChangesAsync();

        var salida = new AgregarClienteOutput
        {
            Nombre = entrada.Nombre,
            CI = entrada.CI
        };

           return CreatedAtAction(nameof(GetClientes), new { id = entrada.IdCliente }, salida);
        }

        // ACTUALIZAR CLIENTE
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

        //  ELIMINAR CLIENTE
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
