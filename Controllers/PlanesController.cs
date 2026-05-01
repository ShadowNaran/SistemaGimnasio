using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GimnasioApi.Data;
using GimnasioApi.Entidades;
using GimnasioApi.DTO.Plan.ListarPlanes;
using GimnasioApi.DTO.Plan.AgregarPlan;

namespace GimnasioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanesController : ControllerBase
    {
        private readonly AppDbContext _contexto;

        //Constructor
        public PlanesController(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        // lista de planes
        [HttpGet]
    public async Task<ActionResult<IEnumerable<PlanOutput>>> GetPlanes()
    {
        var planes = await _contexto.Planes
            .Select(p => new PlanOutput
            {
                IdPlan = p.IdPlan,
                Nombre = p.Nombre,
                Precio = p.Precio,
                Descripcion = p.Descripcion
            })
            .ToListAsync();

        return Ok(planes);
    }


        // plan por id
        [HttpGet("{id}")]
        public async Task<ActionResult<Plan>> GetPlan(int id)
        {
            
            var plan = await _contexto.Planes.FindAsync(id);

            if (plan == null)
                return NotFound();

            return Ok(plan);
        }

        

        // actualizar plan
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlan(int id, [FromBody] Plan plan)
        {
            

            var existing = await _contexto.Planes.FindAsync(id);
            if (existing == null)
                return NotFound("el id no coincide");

            // Actualizar propiedades
            existing.Nombre = plan.Nombre;
            existing.Precio = plan.Precio;
            existing.Descripcion = plan.Descripcion;

            await _contexto.SaveChangesAsync();
            return NoContent();
        }

            

        [HttpPost]
        public async Task<ActionResult<AgregarPlanOutput>> CreatePlan([FromBody] AgregarPlanInput entrada)
        {
            var nuevoPlan = new Plan
            {
                Nombre = entrada.Nombre,
                Precio = entrada.Precio,
                Descripcion = entrada.Descripcion,
                Activo = true 
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
        [HttpGet("Vigente")]
    public async Task<ActionResult<IEnumerable<PlanOutput>>> GetPlanes([FromQuery] bool? activo)
    {
       
        var query = _contexto.Planes.AsQueryable();

        
        if (activo.HasValue)
        {
            query = query.Where(p => p.Activo == activo.Value);
        }

        // Seleccionamos solo los campos necesarios para el Output
        var planes = await query.Select(p => new PlanOutput
        {
            IdPlan = p.IdPlan,
            Nombre = p.Nombre,
            Precio = p.Precio,
            Descripcion = p.Descripcion,
            Activo = p.Activo
        }).ToListAsync();

        return Ok(planes);
    }
        
       
    }
}