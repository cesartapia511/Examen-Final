using System.ComponentModel.DataAnnotations;

namespace TransitoTarija.DTOs;

public class PersonaInvolucradaDto
{
    [Range(1, int.MaxValue)]
    public int AccidenteId { get; set; }

    public int? VehiculoId { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombres { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Apellidos { get; set; } = string.Empty;

    [Required]
    [StringLength(25)]
    public string Ci { get; set; } = string.Empty;

    [StringLength(20)]
    public string? Telefono { get; set; }

    [StringLength(30)]
    public string? Rol { get; set; }

    [StringLength(30)]
    public string? NumeroLicencia { get; set; }

    [StringLength(10)]
    public string? CategoriaLicencia { get; set; }

    [Range(0, 99.99)]
    public decimal? GradoAlcoholemiaGral { get; set; }

    [StringLength(20)]
    public string? EstadoAlcoholemia { get; set; }

    [StringLength(30)]
    public string? EstadoSalud { get; set; }

    [StringLength(120)]
    public string? CentroMedicoTraslado { get; set; }

    public string? DiagnosticoPreliminar { get; set; }
}
