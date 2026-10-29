using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransitoTarija.DTOs;
using TransitoTarija.Interfaces;

namespace TransitoTarija.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/personas-involucradas")]
public class PersonasInvolucradasController : ControllerBase
{
    private readonly IPersonaInvolucradaService _service;

    public PersonasInvolucradasController(IPersonaInvolucradaService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] PersonaInvolucradaDto dto)
    {
        try
        {
            var persona = await _service.CrearAsync(dto);
            return Created($"/api/v1/personas-involucradas/{persona.Id}", persona);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPatch("{id:int}/alcoholemia")]
    public async Task<IActionResult> ActualizarAlcoholemia(
        int id,
        [FromBody] ActualizarAlcoholemiaDto dto)
    {
        var persona = await _service.ActualizarAlcoholemiaAsync(
            id,
            dto.GradoAlcoholemiaGral,
            dto.EstadoAlcoholemia);

        if (persona == null)
            return NotFound(new { mensaje = "No se encontró la persona involucrada." });

        return Ok(persona);
    }
}
