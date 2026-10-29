using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransitoTarija.DTOs;
using TransitoTarija.Interfaces;

namespace TransitoTarija.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/peritajes")]
public class PeritajesTecnicosController : ControllerBase
{
    private readonly IPeritajeTecnicoService _service;

    public PeritajesTecnicosController(IPeritajeTecnicoService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] PeritajeTecnicoDto dto)
    {
        try
        {
            var peritaje = await _service.CrearAsync(dto);

            return Created(
                $"/api/v1/peritajes/{peritaje.Id}",
                peritaje);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }
}