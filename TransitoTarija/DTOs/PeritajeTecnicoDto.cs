using System.ComponentModel.DataAnnotations;

namespace TransitoTarija.DTOs;

public class PeritajeTecnicoDto
{
    [Range(1, int.MaxValue)]
    public int AccidenteId { get; set; }

    [Range(1, int.MaxValue)]
    public int PeritoId { get; set; }

    public DateOnly FechaPeritaje { get; set; }

    [Range(0, 999.99)]
    public decimal? VelocidadEstimadaImpactoKmh { get; set; }

    [Range(0, 999.99)]
    public decimal? HuellaFrenadoMetros { get; set; }

    [Required]
    public string FactorDeterminante { get; set; } = string.Empty;

    [Required]
    public string Conclusiones { get; set; } = string.Empty;

    [StringLength(255)]
    public string? CroquisUrl { get; set; }
}
