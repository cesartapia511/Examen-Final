using Bogus;
using Microsoft.EntityFrameworkCore;
using TransitoTarija.Data;
using TransitoTarija.Models;

namespace TransitoTarija.Seeders;

public static class PeritajesSeeder
{
    public static async Task<int> GenerarAsync(AppDbContext context)
    {
        const int cantidadObjetivo = 300;

        var cantidadActual = await context.PeritajesTecnicos.CountAsync();

        var cantidadFaltante = cantidadObjetivo - cantidadActual;

        if (cantidadFaltante <= 0)
            return 0;

        var accidentes = await context.Accidentes
            .Where(a => a.Peritaje == null)
            .Select(a => new { a.Id, a.CodigoCaso, a.FechaHora })
            .ToListAsync();

        var peritos = await context.Oficiales
            .Select(o => o.Id)
            .ToListAsync();

        if (accidentes.Count == 0 || peritos.Count == 0)
        {
            Console.WriteLine("No existen accidentes sin peritaje u oficiales registrados.");
            return 0;
        }

        var faker = new Faker("es");
        var hoy = DateOnly.FromDateTime(DateTime.UtcNow);
        var peritajes = new List<PeritajeTecnico>();

        foreach (var accidente in faker.PickRandom(accidentes, Math.Min(cantidadFaltante, accidentes.Count)))
        {
            var velocidad = Math.Round(faker.Random.Decimal(20m, 120m), 2);
            var fecha = DateOnly.FromDateTime(accidente.FechaHora.AddDays(faker.Random.Int(1, 10)));

            peritajes.Add(new PeritajeTecnico
            {
                AccidenteId = accidente.Id,
                PeritoId = faker.PickRandom(peritos),
                FechaPeritaje = fecha > hoy ? hoy : fecha,
                VelocidadEstimadaImpactoKmh = velocidad,
                HuellaFrenadoMetros = Math.Round(faker.Random.Decimal(2m, 40m), 2),
                FactorDeterminante = faker.PickRandom(
                    "Exceso de velocidad",
                    "Inobservancia a la señalética de prioridad vial",
                    "Conducción en estado de ebriedad",
                    "Distracción del conductor",
                    "Falla mecánica del vehículo"),
                Conclusiones = $"El vehículo ingresó al cruce a una velocidad estimada de {velocidad} km/h, perdiendo el control al realizar la maniobra evasiva.",
                CroquisUrl = $"https://storage.transito-tarija.gob.bo/croquis/{accidente.CodigoCaso}.pdf",
                CreatedAt = DateTime.UtcNow
            });
        }

        await context.PeritajesTecnicos.AddRangeAsync(peritajes);
        await context.SaveChangesAsync();

        return peritajes.Count;
    }
}

