using TransitoTarija.DTOs;
using TransitoTarija.Interfaces;
using TransitoTarija.Models;

namespace TransitoTarija.Services;

public class AccidenteService : IAccidenteService
{
    public Task<List<Accidente>> ObtenerTodosAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Accidente?> ObtenerPorIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Accidente> CrearAsync(AccidenteDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ActualizarEstadoAsync(int id, string estado)
    {
        throw new NotImplementedException();
    }
}