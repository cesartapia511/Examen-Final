using Microsoft.EntityFrameworkCore;
using TransitoTarija.Data;
using TransitoTarija.DTOs;
using TransitoTarija.Models;

namespace TransitoTarija.Services
{
    public class OficialService : IOficialService
    {
        private readonly AppDbContext _context;

        public OficialService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Oficial> RegistrarOficial(OficialDto dto)
        {
            bool existe = await _context.Oficiales
                .AnyAsync(o => o.NumeroEscalafon == dto.NumeroEscalafon);

            if (existe)
            {
                throw new InvalidOperationException(
                    "Ya existe un oficial con ese número de escalafón."
                );
            }

            var oficial = new Oficial
            {
                Nombres = dto.Nombres,
                Apellidos = dto.Apellidos,
                Grado = dto.Grado,
                NumeroEscalafon = dto.NumeroEscalafon,
                Cargo = dto.Cargo,
                Unidad = dto.Unidad,
                Activo = dto.Activo
            };

            _context.Oficiales.Add(oficial);
            await _context.SaveChangesAsync();

            return oficial;
        }
    }
}
