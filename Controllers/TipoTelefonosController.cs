using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GimnasioApi.Data;
using GimnasioApi.Entidades;
using GimnasioApi.DTO.TipoTelefono.AgregarTipo;

namespace GimnasioApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoTelefonosController : ControllerBase
{
    private readonly AppDbContext _context;
    public TipoTelefonosController(AppDbContext context) { _context = context; }

    [HttpGet]
    
    public async Task<IActionResult> GetTipos()
    {
      return  Ok(await _context.TipoTelefonos.ToListAsync());
    } 

    
    [HttpPost] 
    public async Task<IActionResult> CrearTipo([FromBody] AgregarTipoTelefonoInput entrada)
    {
        var tipo = new TipoTelefono 
        { 
            Nombre = entrada.Nombre 
        };
        
        _context.TipoTelefonos.Add(tipo);
        await _context.SaveChangesAsync();
        
        return Ok(tipo);
    }
}