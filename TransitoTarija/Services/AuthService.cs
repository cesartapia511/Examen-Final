using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TransitoTarija.Data;
using TransitoTarija.DTOs;
using TransitoTarija.Interfaces;

namespace TransitoTarija.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginDto dto)
    {
        var oficial = await _context.Oficiales
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.NumeroEscalafon == dto.NumeroEscalafon && o.Activo);

        if (oficial == null)
            return null;

        if (dto.Password != _configuration["Auth:Password"])
            return null;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var expiracion = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Jwt:ExpiraMinutos"));

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, oficial.Id.ToString()),
            new Claim("numeroEscalafon", oficial.NumeroEscalafon),
            new Claim(ClaimTypes.Name, $"{oficial.Nombres} {oficial.Apellidos}")
        };

        var jwt = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiracion,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        return new LoginResponseDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwt),
            Expiracion = expiracion,
            OficialId = oficial.Id,
            Nombres = oficial.Nombres,
            Apellidos = oficial.Apellidos,
            Grado = oficial.Grado,
            Cargo = oficial.Cargo
        };
    }
}

