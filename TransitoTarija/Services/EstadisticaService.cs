using Microsoft.EntityFrameworkCore;
using TransitoTarija.Data;
using TransitoTarija.Interfaces;

namespace TransitoTarija.Services;

public class EstadisticaService : IEstadisticaService
{
    private readonly AppDbContext _context;

    public EstadisticaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<object> ObtenerPuntosCriticosAsync(int anio)
    {
        if (anio < 1 || anio > 9998)
            return new List<object>();

        var inicio = new DateTime(anio, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var fin = inicio.AddYears(1);

        return await _context.Accidentes
            .AsNoTracking()
            .Where(a => a.FechaHora >= inicio && a.FechaHora < fin)
            .GroupBy(a => a.ZonaBarrio)
            .Select(g => new
            {
                ZonaBarrio = g.Key,
                TotalAccidentes = g.Count(),
                Latitud = g.Average(a => a.Latitud),
                Longitud = g.Average(a => a.Longitud)
            })
            .OrderByDescending(p => p.TotalAccidentes)
            .ToListAsync();
    }
}
