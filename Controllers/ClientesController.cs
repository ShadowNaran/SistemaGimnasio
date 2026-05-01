using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GimnasioApi.Data;
using GimnasioApi.Entidades;
using GimnasioApi.DTO.Cliente.AgregarCliente;
using GimnasioApi.DTO.Plan.AsignarPlan;
using GimnasioApi.DTO.Cliente.ListarClientes;
using GimnasioApi.DTO.Cliente.ObtenerCliente;
using GimnasioApi.DTO.Cliente.ActualizarCliente;

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
       public async Task<ActionResult<IEnumerable<ListarClientesOutput>>> GetClientes()
  {
    var clientes = await _context.Clientes
        .Select(c => new ListarClientesOutput
        {
            IdCliente = c.IdCliente,
            Nombre = c.Nombre,
            CI = c.CI
        })
        .ToListAsync();

    return Ok(clientes);
  }

        //  BUSQUEDA POR ID
       [HttpGet("{id}")]
public async Task<ActionResult<ObtenerClienteOutput>> GetCliente(int id)
{
    var cliente = await _context.Clientes.FindAsync(id);

    if (cliente == null)
        return NotFound();

    var salida = new ObtenerClienteOutput
    {
        IdCliente = cliente.IdCliente,
        Nombre = cliente.Nombre,
        CI = cliente.CI
    };

    return Ok(salida);
}

          //agregar un nuevo cliente
     [HttpPost]
    public async Task<ActionResult<AgregarClienteOutput>> CreateCliente([FromBody] AgregarClienteInput cliente)
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
            IdCliente = entrada.IdCliente,
            Nombre = entrada.Nombre,
            CI = entrada.CI
        };

        return CreatedAtAction(nameof(GetCliente), new { id = entrada.IdCliente }, salida);
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
public async Task<ActionResult<ActualizarClienteOutput>> UpdateCliente(int id, [FromBody] ActualizarClienteInput cliente)
{
    var existing = await _context.Clientes.FindAsync(id);

    if (existing == null)
        return NotFound("El cliente no existe");

    existing.Nombre = cliente.Nombre;
    existing.CI = cliente.CI;

    await _context.SaveChangesAsync();

    var salida = new ActualizarClienteOutput
    {
        IdCliente = existing.IdCliente,
        Nombre = existing.Nombre,
        CI = existing.CI
    };

    return Ok(salida);
}

       [HttpGet("Estado")]
public async Task<ActionResult<List<ListarClientesOutput>>> Get([FromQuery] bool? activo)
{
    // Creamos la consulta base
    var query = _context.Clientes.AsQueryable();

    // Si el usuario envía el parámetro ?activo=true o ?activo=false, filtramos
    if (activo.HasValue)
    {
        query = query.Where(x => x.Activo == activo.Value);
    }

    var clientes = await query
        .Select(c => new ListarClientesOutput
        {
            IdCliente = c.IdCliente,
            Nombre = c.Nombre,
            CI = c.CI,
            Activo = c.Activo   
        }).ToListAsync();

    return clientes;
}
    }
}
