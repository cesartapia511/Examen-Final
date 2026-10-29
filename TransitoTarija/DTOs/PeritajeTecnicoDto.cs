namespace TransitoTarija.DTOs;

public class PeritajeTecnicoDto
{
    public int AccidenteId { get; set; }
    public int PeritoId { get; set; }
    public DateOnly FechaPeritaje { get; set; }
    public decimal? VelocidadEstimadaImpactoKmh { get; set; }
    public decimal? HuellaFrenadoMetros { get; set; }
    public string FactorDeterminante { get; set; } = string.Empty;
    public string Conclusiones { get; set; } = string.Empty;
    public string? CroquisUrl { get; set; }
}