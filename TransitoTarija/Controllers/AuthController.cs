using Microsoft.AspNetCore.Mvc;
using TransitoTarija.DTOs;
using TransitoTarija.Interfaces;

namespace TransitoTarija.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var respuesta = await _service.LoginAsync(dto);

        if (respuesta == null)
            return Unauthorized(new { mensaje = "Número de escalafón o contraseña incorrectos." });

        return Ok(respuesta);
    }
}

