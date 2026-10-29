
using System.ComponentModel.DataAnnotations;

namespace TransitoTarija.DTOs;

public class VehiculoInvolucradoDto
{
    [Range(1, int.MaxValue)]
    public int AccidenteId { get; set; }

    [Required]
    public string Placa { get; set; } = string.Empty;

    public string? Marca { get; set; }
    public string? Modelo { get; set; }
    public string? TipoVehiculo { get; set; }
    public string? Color { get; set; }
    public string? NumeroChasis { get; set; }
    public bool? SoatVigente { get; set; }
    public string? Aseguradora { get; set; }
    public string? DescripcionDanios { get; set; }
    public string? RemolcadoA { get; set; }
}
