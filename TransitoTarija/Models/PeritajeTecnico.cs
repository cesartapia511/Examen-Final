namespace TransitoTarija.Models;

public class PeritajeTecnico
{
    public int Id { get; set; }

    public int AccidenteId { get; set; }
    public Accidente Accidente { get; set; } = null!;

    public int PeritoId { get; set; }
    public Oficial Perito { get; set; } = null!;

    public DateOnly FechaPeritaje { get; set; }

    public decimal? VelocidadEstimadaImpactoKmh { get; set; }
    public decimal? HuellaFrenadoMetros { get; set; }

    public string FactorDeterminante { get; set; } = string.Empty;
    public string Conclusiones { get; set; } = string.Empty;
    public string? CroquisUrl { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}