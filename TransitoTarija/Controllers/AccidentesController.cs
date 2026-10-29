
using Microsoft.AspNetCore.Mvc;
using TransitoTarija.DTOs;
using TransitoTarija.Interfaces;

namespace TransitoTarija.Controllers;

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
    public async Task<IActionResult> ObtenerTodos()
    {
        var accidentes = await _service.ObtenerTodosAsync();
        return Ok(accidentes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var accidente = await _service.ObtenerPorIdAsync(id);

        if (accidente == null)
            return NotFound();

        return Ok(accidente);
    }

    [HttpPost]
    public async Task<IActionResult> Crear(AccidenteDto dto)
    {
        var accidente = await _service.CrearAsync(dto);
        return Ok(accidente);
    }

    [HttpPatch("{id}/estado")]
    public async Task<IActionResult> ActualizarEstado(int id, [FromBody] ActualizarEstadoDto dto)
    {
        var actualizado = await _service.ActualizarEstadoAsync(id, dto.EstadoCaso);

        if (!actualizado)
            return NotFound();

        return Ok(new { mensaje = "Estado actualizado correctamente" });
    }
}

public class ActualizarEstadoDto
{
    public string EstadoCaso { get; set; } = string.Empty;
}
