
using TransitoTarija.DTOs;
using TransitoTarija.Models;

namespace TransitoTarija.Interfaces;

public interface IAccidenteService
{
    Task<List<Accidente>> ObtenerTodosAsync(string? distrito, string? gravedad, DateTime? fechaDesde);

    Task<Accidente?> ObtenerPorIdAsync(int id);

    Task<Accidente> CrearAsync(AccidenteDto dto);

    Task<Accidente?> ActualizarEstadoAsync(int id, string estado);
}
