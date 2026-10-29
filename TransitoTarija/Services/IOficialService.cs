using TransitoTarija.DTOs;
using TransitoTarija.Models;

namespace TransitoTarija.Services
{
    public interface IOficialService
    {
        Task<Oficial> RegistrarOficial(OficialDto dto);
    }
}
