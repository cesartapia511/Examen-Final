using System.ComponentModel.DataAnnotations;

namespace TransitoTarija.DTOs;

public class LoginDto
{
    [Required]
    public string NumeroEscalafon { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}

