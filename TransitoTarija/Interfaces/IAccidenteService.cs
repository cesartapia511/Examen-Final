
using TransitoTarija.DTOs;
using TransitoTarija.Models;

namespace TransitoTarija.Interfaces;

public interface IAccidenteService
{
    Task<List<Accidente>> ObtenerTodosAsync(string? distrito = null, string? gravedad = null, DateTime? fechaDesde = null);

    Task<Accidente?> ObtenerPorIdAsync(int id);

    Task<Accidente> CrearAsync(AccidenteDto dto);

    Task<bool> ActualizarEstadoAsync(int id, string estado);
}
