using Bogus;
using Microsoft.EntityFrameworkCore;
using TransitoTarija.Data;
using TransitoTarija.Models;

namespace TransitoTarija.Seeders;

public static class OficialesSeeder
{
    public static async Task<int> GenerarAsync(AppDbContext context)
    {
        const int cantidadObjetivo = 300;

        var cantidadActual = await context.Oficiales.CountAsync();

        var cantidadFaltante = cantidadObjetivo - cantidadActual;

        if (cantidadFaltante <= 0)
            return 0;

        var escalafones = (await context.Oficiales
            .Select(o => o.NumeroEscalafon)
            .ToListAsync()).ToHashSet();

        var faker = new Faker("es");
        var oficiales = new List<Oficial>();

        if (!escalafones.Contains("ESC-78921"))
        {
            escalafones.Add("ESC-78921");
            oficiales.Add(new Oficial
            {
                Nombres = "Marco Antonio",
                Apellidos = "Vargas Cruz",
                Grado = "Sargento Mayor",
                NumeroEscalafon = "ESC-78921",
                Cargo = "Investigador Especial",
                Unidad = "División Accidentes",
                Activo = true,
                CreatedAt = DateTime.UtcNow
            });
        }

        while (oficiales.Count < cantidadFaltante)
        {
            var escalafon = $"ESC-{faker.Random.Int(10000, 99999)}";

            if (!escalafones.Add(escalafon))
                continue;

            oficiales.Add(new Oficial
            {
                Nombres = faker.Name.FirstName(),
                Apellidos = $"{faker.Name.LastName()} {faker.Name.LastName()}",
                Grado = faker.PickRandom(
                    "Policía", "Cabo", "Sargento Segundo", "Sargento Primero",
                    "Subteniente", "Teniente", "Capitán", "Mayor"),
                NumeroEscalafon = escalafon,
                Cargo = faker.PickRandom(
                    "Investigador", "Perito", "Jefe de Turno", "Agente de Tránsito"),
                Unidad = faker.PickRandom(
                    "División Accidentes", "Peritajes Técnicos", "Tránsito Tarija", "Seguridad Vial"),
                Activo = faker.Random.Bool(0.9f),
                CreatedAt = DateTime.UtcNow
            });
        }

        await context.Oficiales.AddRangeAsync(oficiales);
        await context.SaveChangesAsync();

        return oficiales.Count;
    }
}

