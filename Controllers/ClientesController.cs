using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GimnasioApi.Data;
using GimnasioApi.Entidades;
using GimnasioApi.DTO.Cliente.AgregarCliente;
using GimnasioApi.DTO.Plan.AsignarPlan;
using GimnasioApi.DTO.Cliente.ListarClientes;
using GimnasioApi.DTO.Cliente.ObtenerCliente;
using GimnasioApi.DTO.Cliente.ActualizarCliente;
using GimnasioApi.DTO.Cliente.AgregarTelefono;

namespace GimnasioApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClientesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ListarClientesOutput>>> GetClientes()
    {
        var clientes = await _context.Clientes
            .Select(c => new ListarClientesOutput
            {
                IdCliente = c.IdCliente,
                Nombre = c.Nombre,
                CI = c.CI,
                Activo = c.Activo
            })
            .ToListAsync();
        return Ok(clientes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ObtenerClienteOutput>> GetCliente(int id)
    {
        
        var cliente = await _context.Clientes
            .Include(c => c.Telefonos)
            .Include(c => c.ClientesPlanes)
                .ThenInclude(cp => cp.Plan)
            .FirstOrDefaultAsync(c => c.IdCliente == id);

        if (cliente == null)
            return NotFound("El cliente no existe.");

        var salida = new ObtenerClienteOutput
        {
            IdCliente = cliente.IdCliente,
            Nombre = cliente.Nombre,
            CI = cliente.CI,
            Activo = cliente.Activo,
            Telefonos = cliente.Telefonos.Select(t => t.Numero).ToList(),
            PlanesActivos = cliente.ClientesPlanes
                .Where(cp => cp.Activo == true)
                .Select(cp => cp.Plan.Nombre)
                .ToList()
        };

        return Ok(salida);
    }

    [HttpPost]
    public async Task<ActionResult<AgregarClienteOutput>> CreateCliente([FromBody] AgregarClienteInput cliente)
    { 
        var existeCi = await _context.Clientes.AnyAsync(c => c.CI == cliente.CI);
    
        if (existeCi)
        {
       
        return BadRequest("Ya existe un cliente registrado con este numero de CI.");
         }
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

    [HttpPost("{id}/telefonos")]
    public async Task<IActionResult> AgregarTelefono(int id, [FromBody] AgregarTelefonoInput entrada)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente == null)
            return NotFound($"No se encontro el cliente con ID {id}");

        var tipoTelefono = await _context.TipoTelefonos.FindAsync(entrada.IdTipoTelefono);
        if (tipoTelefono == null)
            return BadRequest($"El ID TipoTelefonos no se encuentra.");

        var nuevoTelefono = new Telefono
        {
            IdCliente = id,
            Numero = entrada.Numero,
            IdTipoTelefono = entrada.IdTipoTelefono,
            Cliente = cliente,
            TipoTelefono = tipoTelefono
        };

        _context.Telefonos.Add(nuevoTelefono);
        await _context.SaveChangesAsync();

        return Ok(new { mensaje = "Telefono agregado correctamente al cliente." });
    }

    [HttpPost("asignar-planes")]
    public async Task<IActionResult> AsignarPlanes([FromBody] AsignarPlanesClienteInput entrada)
    {
        var cliente = await _context.Clientes.FindAsync(entrada.IdCliente);
        if (cliente == null)
            return NotFound($"El ID del cliente no se encuentra.");

        foreach (var item in entrada.Planes)
        {
            var planExiste = await _context.Planes.AnyAsync(p => p.IdPlan == item.IdPlan);
            if (!planExiste)
                return BadRequest($"El ID del plan no se Encuentra.");

            var clientePlan = new ClientePlan
            {
                IdCliente = entrada.IdCliente,
                IdPlan = item.IdPlan,
                Activo = true
            };
            _context.ClientesPlanes.Add(clientePlan);
        }

        await _context.SaveChangesAsync();
        return Ok(new { mensaje = "Planes asignados correctamente" });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ActualizarClienteOutput>> UpdateCliente(int id, [FromBody] ActualizarClienteInput cliente)
    {
        var existing = await _context.Clientes.FindAsync(id);
        if (existing == null)
        {
            return NotFound("El cliente no existe");
        }
        var existeCiEnOtro = await _context.Clientes.AnyAsync(c => c.CI == cliente.CI && c.IdCliente != id);
        if (existeCiEnOtro)
        {
        return BadRequest("El CI ingresado ya le pertenece a otro cliente.");
        }

        existing.Nombre = cliente.Nombre;
        existing.CI = cliente.CI;
        existing.Activo = cliente.Activo;
        await _context.SaveChangesAsync();

        var salida = new ActualizarClienteOutput
        {
            IdCliente = existing.IdCliente,
            Nombre = existing.Nombre,
            CI = existing.CI
        };

        return Ok(salida);
    }
    
   [HttpGet("PorEstado")]
   public async Task<ActionResult<IEnumerable<ListarClientesOutput>>> GetClientesPorEstado([FromQuery] bool estadoActivo)
   {
    
        var clientes = await _context.Clientes
        .Where(c => c.Activo == estadoActivo) 
        .Select(c => new ListarClientesOutput
        {
            IdCliente = c.IdCliente,
            Nombre = c.Nombre,
            CI = c.CI,
            Activo = c.Activo
        })
        .ToListAsync();

       return Ok(clientes);
   }
}