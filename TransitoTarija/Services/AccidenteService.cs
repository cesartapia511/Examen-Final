using Microsoft.EntityFrameworkCore;
using TransitoTarija.Data;
using TransitoTarija.DTOs;
using TransitoTarija.Interfaces;
using TransitoTarija.Models;

namespace TransitoTarija.Services;

public class AccidenteService : IAccidenteService
{
    private readonly AppDbContext _context;

    public AccidenteService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Accidente>> ObtenerTodosAsync(
        string? distrito = null,
        string? gravedad = null,
        DateTime? fechaDesde = null)
    {
        var consulta = _context.Accidentes.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(distrito))
            consulta = consulta.Where(a => a.Distrito == distrito);

        if (!string.IsNullOrWhiteSpace(gravedad))
            consulta = consulta.Where(a => a.Gravedad == gravedad);

        if (fechaDesde.HasValue)
        {
            var desde = ConvertirAUtc(fechaDesde.Value);
            consulta = consulta.Where(a => a.FechaHora >= desde);
        }

        return await consulta
            .OrderByDescending(a => a.FechaHora)
            .ToListAsync();
    }

    public async Task<Accidente?> ObtenerPorIdAsync(int id)
    {
        return await _context.Accidentes
            .AsNoTracking()
            .Include(a => a.OficialAsignado)
            .Include(a => a.Vehiculos)
            .Include(a => a.Personas)
            .Include(a => a.Peritaje)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Accidente> CrearAsync(AccidenteDto dto)
    {
        if (dto.OficialAsignadoId.HasValue)
        {
            var oficialExiste = await _context.Oficiales
                .AnyAsync(o => o.Id == dto.OficialAsignadoId.Value);

            if (!oficialExiste)
                throw new ArgumentException("El oficial asignado no existe.");
        }

        var codigoExiste = await _context.Accidentes
            .AnyAsync(a => a.CodigoCaso == dto.CodigoCaso);

        if (codigoExiste)
            throw new InvalidOperationException("Ya existe un accidente con ese código de caso.");

        var accidente = new Accidente
        {
            CodigoCaso = dto.CodigoCaso,
            FechaHora = ConvertirAUtc(dto.FechaHora),
            TipoAccidente = dto.TipoAccidente,
            Gravedad = dto.Gravedad,
            Distrito = dto.Distrito,
            ZonaBarrio = dto.ZonaBarrio,
            DireccionReferencia = dto.DireccionReferencia,
            Latitud = dto.Latitud,
            Longitud = dto.Longitud,
            CondicionClimatica = dto.CondicionClimatica,
            EstadoCalzada = dto.EstadoCalzada,
            OficialAsignadoId = dto.OficialAsignadoId,
            Observaciones = dto.Observaciones
        };

        _context.Accidentes.Add(accidente);
        await _context.SaveChangesAsync();

        return accidente;
    }

    public async Task<bool> ActualizarEstadoAsync(int id, string estado)
    {
        var accidente = await _context.Accidentes
            .FirstOrDefaultAsync(a => a.Id == id);

        if (accidente == null)
            return false;

        accidente.EstadoCaso = estado;
        await _context.SaveChangesAsync();

        return true;
    }

    private static DateTime ConvertirAUtc(DateTime fecha)
    {
        return fecha.Kind == DateTimeKind.Unspecified
            ? DateTime.SpecifyKind(fecha, DateTimeKind.Utc)
            : fecha.ToUniversalTime();
    }
}
