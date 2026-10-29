
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransitoTarija.DTOs;
using TransitoTarija.Interfaces;

namespace TransitoTarija.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/accidentes")]
public class AccidentesController : ControllerBase
{
    private readonly IAccidenteService _service;

    public AccidentesController(IAccidenteService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos(
        [FromQuery] string? distrito,
        [FromQuery] string? gravedad,
        [FromQuery] DateTime? fechaDesde)
    {
        var accidentes = await _service.ObtenerTodosAsync(distrito, gravedad, fechaDesde);
        return Ok(accidentes);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var accidente = await _service.ObtenerPorIdAsync(id);

        if (accidente == null)
            return NotFound(new { mensaje = "Accidente no encontrado" });

        return Ok(accidente);
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] AccidenteDto dto)
    {
        try
        {
            var accidente = await _service.CrearAsync(dto);
            return Created($"/api/v1/accidentes/{accidente.Id}", accidente);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
    }

    [HttpPatch("{id:int}/estado")]
    public async Task<IActionResult> ActualizarEstado(int id, [FromBody] ActualizarEstadoDto dto)
    {
        try
        {
            var accidente = await _service.ActualizarEstadoAsync(id, dto.EstadoCaso);

            if (accidente == null)
                return NotFound(new { mensaje = "Accidente no encontrado" });

            return Ok(accidente);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }
}
