using Microsoft.EntityFrameworkCore;
using TransitoTarija.Data;
using TransitoTarija.DTOs;
using TransitoTarija.Interfaces;
using TransitoTarija.Models;

namespace TransitoTarija.Services;

public class PersonaInvolucradaService : IPersonaInvolucradaService
{
    private readonly AppDbContext _context;

    public PersonaInvolucradaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PersonaInvolucrada> CrearAsync(PersonaInvolucradaDto dto)
    {
        var accidenteExiste = await _context.Accidentes
            .AnyAsync(a => a.Id == dto.AccidenteId);

        if (!accidenteExiste)
            throw new ArgumentException("El accidente indicado no existe.");

        if (dto.VehiculoId.HasValue)
        {
            var vehiculoExiste = await _context.VehiculosInvolucrados
                .AnyAsync(v => v.Id == dto.VehiculoId.Value &&
                               v.AccidenteId == dto.AccidenteId);

            if (!vehiculoExiste)
                throw new ArgumentException("El vehículo no existe o no pertenece al accidente indicado.");
        }

        var persona = new PersonaInvolucrada
        {
            AccidenteId = dto.AccidenteId,
            VehiculoId = dto.VehiculoId,
            Nombres = dto.Nombres,
            Apellidos = dto.Apellidos,
            Ci = dto.Ci,
            Telefono = dto.Telefono,
            Rol = dto.Rol,
            NumeroLicencia = dto.NumeroLicencia,
            CategoriaLicencia = dto.CategoriaLicencia,
            GradoAlcoholemiaGral = dto.GradoAlcoholemiaGral,
            EstadoAlcoholemia = dto.EstadoAlcoholemia,
            EstadoSalud = dto.EstadoSalud,
            CentroMedicoTraslado = dto.CentroMedicoTraslado,
            DiagnosticoPreliminar = dto.DiagnosticoPreliminar
        };

        _context.PersonasInvolucradas.Add(persona);
        await _context.SaveChangesAsync();

        return persona;
    }

    public async Task<PersonaInvolucrada?> ActualizarAlcoholemiaAsync(
        int id,
        decimal gradoAlcoholemia,
        string estadoAlcoholemia)
    {
        var persona = await _context.PersonasInvolucradas
            .FirstOrDefaultAsync(p => p.Id == id);

        if (persona == null)
            return null;

        persona.GradoAlcoholemiaGral = gradoAlcoholemia;
        persona.EstadoAlcoholemia = estadoAlcoholemia;

        await _context.SaveChangesAsync();

        return persona;
    }
}