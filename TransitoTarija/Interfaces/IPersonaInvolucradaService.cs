using TransitoTarija.DTOs;
using TransitoTarija.Models;

namespace TransitoTarija.Interfaces;

public interface IPersonaInvolucradaService
{
    Task<PersonaInvolucrada> CrearAsync(PersonaInvolucradaDto dto);
    Task<PersonaInvolucrada?> ActualizarAlcoholemiaAsync(int id, decimal gradoAlcoholemia, string estadoAlcoholemia);
}