
using System.ComponentModel.DataAnnotations;

namespace TransitoTarija.DTOs;

public class AccidenteDto
{
    [Required]
    [StringLength(30)]
    public string CodigoCaso { get; set; } = string.Empty;

    public DateTime FechaHora { get; set; }

    [Required]
    [StringLength(60)]
    public string TipoAccidente { get; set; } = string.Empty;

    [Required]
    [StringLength(30)]
    public string Gravedad { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string ZonaBarrio { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    public string DireccionReferencia { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Distrito { get; set; } = string.Empty;

    [Range(-90, 90)]
    public decimal? Latitud { get; set; }

    [Range(-180, 180)]
    public decimal? Longitud { get; set; }

    [StringLength(40)]
    public string? CondicionClimatica { get; set; }

    [StringLength(40)]
    public string? EstadoCalzada { get; set; }

    public int? OficialAsignadoId { get; set; }
    public string? Observaciones { get; set; }
}
