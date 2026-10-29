
namespace TransitoTarija.DTOs;

public class AccidenteDto
{
    public string CodigoCaso { get; set; } = string.Empty;
    public DateTime FechaHora { get; set; }
    public string TipoAccidente { get; set; } = string.Empty;
    public string Gravedad { get; set; } = string.Empty;
    public string ZonaBarrio { get; set; } = string.Empty;
    public string DireccionReferencia { get; set; } = string.Empty;
    public string Distrito { get; set; } = string.Empty;
    public decimal? Latitud { get; set; }
    public decimal? Longitud { get; set; }
    public string? CondicionClimatica { get; set; }
    public string? EstadoCalzada { get; set; }
    public int? OficialAsignadoId { get; set; }
    public string? Observaciones { get; set; }
}
