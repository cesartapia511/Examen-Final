namespace TransitoTarija.DTOs;

public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiracion { get; set; }
    public int OficialId { get; set; }
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Grado { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
}

