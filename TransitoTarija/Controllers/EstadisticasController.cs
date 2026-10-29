using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransitoTarija.Interfaces;

namespace TransitoTarija.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/estadisticas")]
public class EstadisticasController : ControllerBase
{
    private readonly IEstadisticaService _service;

    public EstadisticasController(IEstadisticaService service)
    {
        _service = service;
    }

    [HttpGet("puntos-criticos")]
    public async Task<IActionResult> ObtenerPuntosCriticos([FromQuery] int anio)
    {
        if (anio < 1 || anio > 9998)
            return BadRequest(new { mensaje = "Debe indicar un año válido en el parámetro anio." });

        var resultado = await _service.ObtenerPuntosCriticosAsync(anio);
        return Ok(resultado);
    }
}
