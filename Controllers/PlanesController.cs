using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GimnasioApi.Data;
using GimnasioApi.Entidades;
using GimnasioApi.DTO.Plan.ListarPlanes;
using GimnasioApi.DTO.Plan.AgregarPlan;
using GimnasioApi.DTO.Plan.ObtenerPlan;
using GimnasioApi.DTO.Plan.ActualizarPlan;

namespace GimnasioApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlanesController : ControllerBase
{
    private readonly AppDbContext _contexto;

    public PlanesController(AppDbContext contexto)
    {
        _contexto = contexto;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlanOutput>>> GetPlanes()
    {
        var planes = await _contexto.Planes
            .Select(p => new PlanOutput
            {
                IdPlan = p.IdPlan,
                Nombre = p.Nombre,
                Precio = p.Precio,
                Descripcion = p.Descripcion,
                Activo = p.Activo
            })
            .ToListAsync();
        return Ok(planes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ObtenerPlanOutput>> GetPlan(int id)
    {
        var plan = await _contexto.Planes.FindAsync(id);
        if (plan == null)
            return NotFound("El plan no existe.");

        var salida = new ObtenerPlanOutput
        {
            IdPlan = plan.IdPlan,
            Nombre = plan.Nombre,
            Precio = plan.Precio,
            Descripcion = plan.Descripcion,
            Activo = plan.Activo
        };

        return Ok(salida);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePlan(int id, [FromBody] ActualizarPlanInput plan)
    {
        var existing = await _contexto.Planes.FindAsync(id);
        if (existing == null)
        {
            return NotFound("El ID del plan no coincide o no existe.");
        }
            
    
   
         var existeNombreEnOtro = await _contexto.Planes.AnyAsync(p => p.Nombre.Trim().ToLower() == plan.Nombre.Trim().ToLower() && p.IdPlan != id);
         if (existeNombreEnOtro)
          {
         return BadRequest("Ya existe otro plan distinto con ese nombre.");
          }

        existing.Nombre = plan.Nombre;
        existing.Precio = plan.Precio;
        existing.Descripcion = plan.Descripcion;
        existing.Activo = plan.Activo;
        await _contexto.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<AgregarPlanOutput>> CreatePlan([FromBody] AgregarPlanInput entrada)
    {
        var existePlan = await _contexto.Planes.AnyAsync(p => p.Nombre.Trim().ToLower() == entrada.Nombre.Trim().ToLower());
         if (existePlan)
         {
        return BadRequest("Ya existe un plan registrado con ese nombre.");
         }
        var nuevoPlan = new Plan
        {
            Nombre = entrada.Nombre,
            Precio = entrada.Precio,
            Descripcion = entrada.Descripcion
        };

        _contexto.Planes.Add(nuevoPlan);
        await _contexto.SaveChangesAsync();

        var salida = new AgregarPlanOutput
        {
            IdPlan = nuevoPlan.IdPlan,
            Nombre = nuevoPlan.Nombre,
            Precio = nuevoPlan.Precio,
            Descripcion = nuevoPlan.Descripcion,
            Activo = nuevoPlan.Activo
        };

        return CreatedAtAction(nameof(GetPlan), new { id = salida.IdPlan }, salida);
    }
    [HttpGet("PorEstado")]
    public async Task<ActionResult<IEnumerable<PlanOutput>>> GetPlanesPorEstado([FromQuery] bool estadoActivo)
    {
    
    var PlanesEstado = await _contexto.Planes
        .Where(c => c.Activo == estadoActivo) 
        .Select(c => new PlanOutput
        {
            IdPlan = c.IdPlan,
            Nombre = c.Nombre,
            Precio = c.Precio,
            Descripcion = c.Descripcion,
            Activo = c.Activo
        })
        .ToListAsync();

    return Ok(PlanesEstado);  
    }
}