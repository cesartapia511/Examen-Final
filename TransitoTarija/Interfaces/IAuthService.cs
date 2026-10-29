using TransitoTarija.DTOs;

namespace TransitoTarija.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto?> LoginAsync(LoginDto dto);
}

