using Microsoft.AspNetCore.Mvc;
using TransitoTarija.DTOs;
using TransitoTarija.Services;

namespace TransitoTarija.Controllers
{
    [ApiController]
    [Route("api/v1/oficiales")]
    public class OficialesController : ControllerBase
    {
        private readonly IOficialService _oficialService;

        public OficialesController(IOficialService oficialService)
        {
            _oficialService = oficialService;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarOficial(
            [FromBody] OficialDto dto)
        {
            try
            {
                var oficial = await _oficialService.RegistrarOficial(dto);

                return Created(
                    $"/api/v1/oficiales/{oficial.Id}",
                    new
                    {
                        mensaje = "Oficial registrado correctamente.",
                        oficial
                    }
                );
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }
    }
}
