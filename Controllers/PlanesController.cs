using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GimnasioApi.Data;
using GimnasioApi.Entidades;

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
        public async Task<ActionResult<ICollection<Plan>>> GetPlanes()
        {
            var planes = await _contexto.Planes.ToListAsync();
            return Ok(planes);
        }

        // plan por id
        [HttpGet("{id}")]
        public async Task<ActionResult<Plan>> GetPlan(int id)
        {
            // FindAsync(id) → busca un registro por su clave primaria
            var plan = await _contexto.Planes.FindAsync(id);

            if (plan == null)
                return NotFound();

            return Ok(plan);
        }

        // agregar plan nuevo
        [HttpPost]
        public async Task<ActionResult<Plan>> CreatePlan([FromBody] Plan plan)
        {
            _contexto.Planes.Add(plan);
            await _contexto.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPlan), new { id = plan.IdPlan }, plan);
        }

        // actualizar plan
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlan(int id, [FromBody] Plan plan)
        {
            if (id != plan.IdPlan)
                return BadRequest("el id no coincide");

            var existing = await _contexto.Planes.FindAsync(id);
            if (existing == null)
                return NotFound();

            // Actualizar propiedades
            existing.Nombre = plan.Nombre;
            existing.Precio = plan.Precio;
            existing.Descripcion = plan.Descripcion;

            await _contexto.SaveChangesAsync();
            return NoContent();
        }

        // eliminar plan
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlan(int id)
        {
            var plan = await _contexto.Planes.FindAsync(id);
            if (plan == null)
                return NotFound();

            _contexto.Planes.Remove(plan);
            await _contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}