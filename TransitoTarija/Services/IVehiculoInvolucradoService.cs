
using TransitoTarija.DTOs;
using TransitoTarija.Models;

namespace TransitoTarija.Services;

public interface IVehiculoInvolucradoService
{
    Task<VehiculoInvolucrado?> RegistrarAsync(VehiculoInvolucradoDto dto);
    Task<List<VehiculoInvolucrado>?> ListarPorAccidenteAsync(int accidenteId);
}
