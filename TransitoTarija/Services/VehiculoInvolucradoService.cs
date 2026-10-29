
using Microsoft.EntityFrameworkCore;
using TransitoTarija.Data;
using TransitoTarija.DTOs;
using TransitoTarija.Models;

namespace TransitoTarija.Services;

public class VehiculoInvolucradoService : IVehiculoInvolucradoService
{
    private readonly AppDbContext _context;

    public VehiculoInvolucradoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<VehiculoInvolucrado?> RegistrarAsync(VehiculoInvolucradoDto dto)
    {
        var existeAccidente = await _context.Accidentes
            .AnyAsync(a => a.Id == dto.AccidenteId);

        if (!existeAccidente)
            return null;

        var vehiculo = new VehiculoInvolucrado
        {
            AccidenteId = dto.AccidenteId,
            Placa = dto.Placa,
            Marca = dto.Marca,
            Modelo = dto.Modelo,
            TipoVehiculo = dto.TipoVehiculo,
            Color = dto.Color,
            NumeroChasis = dto.NumeroChasis,
            SoatVigente = dto.SoatVigente,
            Aseguradora = dto.Aseguradora,
            DescripcionDanios = dto.DescripcionDanios,
            RemolcadoA = dto.RemolcadoA,
            CreatedAt = DateTime.UtcNow
        };

        _context.VehiculosInvolucrados.Add(vehiculo);
        await _context.SaveChangesAsync();

        return vehiculo;
    }

    public async Task<List<VehiculoInvolucrado>?> ListarPorAccidenteAsync(int accidenteId)
    {
        var existeAccidente = await _context.Accidentes
            .AnyAsync(a => a.Id == accidenteId);

        if (!existeAccidente)
            return null;

        return await _context.VehiculosInvolucrados
            .AsNoTracking()
            .Where(v => v.AccidenteId == accidenteId)
            .ToListAsync();
    }
}
