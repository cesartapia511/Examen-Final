using Microsoft.AspNetCore.Mvc;
using TransitoTarija.DTOs;
using TransitoTarija.Interfaces;

namespace TransitoTarija.Controllers;

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
        if (dto.GradoAlcoholemiaGral < 0)
            return BadRequest(new { mensaje = "El grado de alcoholemia no puede ser negativo." });

        var persona = await _service.ActualizarAlcoholemiaAsync(
            id,
            dto.GradoAlcoholemiaGral,
            dto.EstadoAlcoholemia);

        if (persona == null)
            return NotFound(new { mensaje = "No se encontró la persona involucrada." });

        return Ok(persona);
    }
}

public class ActualizarAlcoholemiaDto
{
    public decimal GradoAlcoholemiaGral { get; set; }
    public string EstadoAlcoholemia { get; set; } = string.Empty;
}