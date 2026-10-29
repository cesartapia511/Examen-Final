using TransitoTarija.DTOs;
using TransitoTarija.Services;

public class VehiculoInvolucrado : IVehiculoInvolucradoService
{
    public Task<List<VehiculoInvolucrado>?> ListarPorAccidenteAsync(int accidenteId)
    {
        throw new NotImplementedException();
    }

    public Task<VehiculoInvolucrado?> RegistrarAsync(VehiculoInvolucradoDto dto)
    {
        throw new NotImplementedException();
    }
}