namespace TransitoTarija.Interfaces;

public interface IEstadisticaService
{
    Task<object> ObtenerPuntosCriticosAsync(int anio);
}