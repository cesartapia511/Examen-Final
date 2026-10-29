using TransitoTarija.DTOs;
using TransitoTarija.Models;

namespace TransitoTarija.Interfaces;

public interface IPeritajeTecnicoService
{
    Task<PeritajeTecnico> CrearAsync(PeritajeTecnicoDto dto);
    Task<PeritajeTecnico?> ObtenerPorAccidenteAsync(int accidenteId);
}