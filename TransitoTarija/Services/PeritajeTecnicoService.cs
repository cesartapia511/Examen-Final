using Microsoft.EntityFrameworkCore;
using TransitoTarija.Data;
using TransitoTarija.DTOs;
using TransitoTarija.Interfaces;
using TransitoTarija.Models;

namespace TransitoTarija.Services;

public class PeritajeTecnicoService : IPeritajeTecnicoService
{
    private readonly AppDbContext _context;

    public PeritajeTecnicoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PeritajeTecnico> CrearAsync(PeritajeTecnicoDto dto)
    {
        var accidenteExiste = await _context.Accidentes
            .AnyAsync(a => a.Id == dto.AccidenteId);

        if (!accidenteExiste)
            throw new ArgumentException("El accidente indicado no existe.");

        var peritoExiste = await _context.Oficiales
            .AnyAsync(o => o.Id == dto.PeritoId);

        if (!peritoExiste)
            throw new ArgumentException("El oficial indicado como perito no existe.");

        var peritajeExistente = await _context.PeritajesTecnicos
            .AnyAsync(p => p.AccidenteId == dto.AccidenteId);

        if (peritajeExistente)
            throw new ArgumentException("Este accidente ya tiene un peritaje técnico registrado.");

        var peritaje = new PeritajeTecnico
        {
            AccidenteId = dto.AccidenteId,
            PeritoId = dto.PeritoId,
            FechaPeritaje = dto.FechaPeritaje,
            VelocidadEstimadaImpactoKmh = dto.VelocidadEstimadaImpactoKmh,
            HuellaFrenadoMetros = dto.HuellaFrenadoMetros,
            FactorDeterminante = dto.FactorDeterminante,
            Conclusiones = dto.Conclusiones,
            CroquisUrl = dto.CroquisUrl
        };

        _context.PeritajesTecnicos.Add(peritaje);
        await _context.SaveChangesAsync();

        return peritaje;
    }

    public async Task<PeritajeTecnico?> ObtenerPorAccidenteAsync(int accidenteId)
    {
        return await _context.PeritajesTecnicos
            .Include(p => p.Perito)
            .FirstOrDefaultAsync(p => p.AccidenteId == accidenteId);
    }
}