
using Microsoft.AspNetCore.Mvc;
using TransitoTarija.DTOs;
using TransitoTarija.Services;

namespace TransitoTarija.Controllers;

[ApiController]
[Route("api/v1/vehiculos-involucrados")]
public class VehiculosInvolucradosController : ControllerBase
{
    private readonly IVehiculoInvolucradoService _service;

    public VehiculosInvolucradosController(IVehiculoInvolucradoService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Registrar(VehiculoInvolucradoDto dto)
    {
        var vehiculo = await _service.RegistrarAsync(dto);

        if (vehiculo == null)
            return BadRequest(new { mensaje = "El accidente no existe" });

        return StatusCode(201, vehiculo);
    }

    [HttpGet("/api/v1/accidentes/{id:int}/vehiculos")]
    public async Task<IActionResult> ListarPorAccidente(int id)
    {
        var vehiculos = await _service.ListarPorAccidenteAsync(id);

        if (vehiculos == null)
            return NotFound(new { mensaje = "Accidente no encontrado" });

        return Ok(vehiculos);
    }
}
