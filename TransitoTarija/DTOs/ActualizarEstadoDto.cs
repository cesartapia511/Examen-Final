using System.ComponentModel.DataAnnotations;

namespace TransitoTarija.DTOs;

public class ActualizarEstadoDto
{
    [Required]
    [StringLength(40)]
    public string EstadoCaso { get; set; } = string.Empty;

    public string? JuzgadoOInstitucion { get; set; }
    public string? NumeroCuadernoInvestigacion { get; set; }
    public string? MotivoPase { get; set; }
}

