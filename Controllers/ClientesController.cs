using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GimnasioApi.Data;
using GimnasioApi.Entidades;
using GimnasioApi.DTO.Cliente.AgregarCliente;
using GimnasioApi.DTO.Plan.AsignarPlan;

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

        //  LISTA DE CLIENTES 
       [HttpGet]
           public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
          {
             return await _context.Clientes.ToListAsync(); 
          } 

        //  BUSQUEDA POR ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente( int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

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

       [HttpPost("asignar-planes")]
       public async Task<IActionResult> AsignarPlanes([FromBody] AsignarPlanesClienteInput entrada)
{
    //  validar si el cliente existe
    var cliente = await _context.Clientes.FindAsync(entrada.IdCliente);

    if (cliente == null)
    {
        
        return NotFound($"No se encontro el cliente con ID {entrada.IdCliente}");
    }

    //recorrer la lista de planes del DTO
    foreach (var item in entrada.Planes)
    {
        var clientePlan = new ClientePlan
        {
            IdCliente = entrada.IdCliente,
            IdPlan = item.IdPlan,
            FechaInicio = DateTime.Now, 
            FechaFin = DateTime.Now.AddMonths(1),
            Estado = "Activo"
        };

        _context.ClientesPlanes.Add(clientePlan);
    }

    await _context.SaveChangesAsync();

    return Ok(new { mensaje = "Planes asignados correctamente" });
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
