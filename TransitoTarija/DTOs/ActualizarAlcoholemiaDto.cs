using System.ComponentModel.DataAnnotations;

namespace TransitoTarija.DTOs;

public class ActualizarAlcoholemiaDto
{
    [Range(0, 99.99)]
    public decimal GradoAlcoholemiaGral { get; set; }

    [Required]
    [StringLength(20)]
    public string EstadoAlcoholemia { get; set; } = string.Empty;

    public string? NumeroActaDosaje { get; set; }
    public string? Laboratorio { get; set; }
}

