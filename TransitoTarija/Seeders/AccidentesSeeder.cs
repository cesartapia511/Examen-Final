using Bogus;
using Microsoft.EntityFrameworkCore;
using TransitoTarija.Data;
using TransitoTarija.Models;

namespace TransitoTarija.Seeders;

public static class AccidentesSeeder
{
    private static readonly (string Direccion, string Zona, string Distrito, decimal Latitud, decimal Longitud)[] Puntos =
    {
        ("Av. Panamericana y Av. Integración", "Barrio Miraflores", "Distrito 3", -21.52400000m, -64.74300000m),
        ("Rotonda del Moto Méndez", "Barrio Moto Méndez", "Distrito 1", -21.52870000m, -64.73350000m),
        ("Puente Bolívar", "Barrio El Molino", "Distrito 2", -21.54100000m, -64.72850000m),
        ("Av. Las Américas y C. Juan Misael Saracho", "Barrio San Martín", "Distrito 1", -21.53549200m, -64.72991800m),
        ("Av. Jaime Paz Zamora", "Barrio Aeropuerto", "Distrito 4", -21.55600000m, -64.70800000m),
        ("Puente San Martín", "Barrio San Martín", "Distrito 1", -21.53900000m, -64.72200000m),
        ("Av. Circunvalación", "Barrio Juan XXIII", "Distrito 5", -21.51800000m, -64.71900000m),
        ("Av. Belgrano y C. Bolívar", "Zona Central", "Distrito 1", -21.53330000m, -64.73050000m),
        ("Av. Víctor Paz Estenssoro", "Barrio Las Panosas", "Distrito 6", -21.52100000m, -64.75800000m),
        ("Av. Domingo Paz", "Barrio Luis Pizarro", "Distrito 2", -21.54600000m, -64.73700000m)
    };

    public static async Task<int> GenerarAsync(AppDbContext context)
    {
        const int cantidadObjetivo = 300;

        var cantidadActual = await context.Accidentes.CountAsync();

        var cantidadFaltante = cantidadObjetivo - cantidadActual;

        if (cantidadFaltante <= 0)
            return 0;

        var oficiales = await context.Oficiales
            .Select(o => o.Id)
            .ToListAsync();

        if (oficiales.Count == 0)
        {
            Console.WriteLine("No existen oficiales registrados.");
            return 0;
        }

        var codigos = (await context.Accidentes
            .Select(a => a.CodigoCaso)
            .ToListAsync()).ToHashSet();

        var faker = new Faker("es");
        var accidentes = new List<Accidente>();
        var correlativo = 1;

        var desde = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var hasta = new DateTime(2026, 10, 28, 0, 0, 0, DateTimeKind.Utc);

        while (accidentes.Count < cantidadFaltante)
        {
            var codigo = $"TRJ-ACC-2026-{correlativo++:0000}";

            if (!codigos.Add(codigo))
                continue;

            var punto = faker.PickRandom(Puntos);

            accidentes.Add(new Accidente
            {
                CodigoCaso = codigo,
                FechaHora = DateTime.SpecifyKind(faker.Date.Between(desde, hasta), DateTimeKind.Utc),
                TipoAccidente = faker.PickRandom(
                    "Colisión Frontal", "Colisión Lateral", "Choque por Alcance",
                    "Atropello", "Vuelco", "Choque con Objeto Fijo"),
                Gravedad = faker.Random.WeightedRandom(
                    new[] { "solo_danos", "con_heridos", "fatal" },
                    new[] { 0.5f, 0.35f, 0.15f }),
                Distrito = punto.Distrito,
                ZonaBarrio = punto.Zona,
                DireccionReferencia = punto.Direccion,
                Latitud = Math.Round(punto.Latitud + faker.Random.Decimal(-0.0005m, 0.0005m), 8),
                Longitud = Math.Round(punto.Longitud + faker.Random.Decimal(-0.0005m, 0.0005m), 8),
                CondicionClimatica = faker.PickRandom("Despejado", "Nublado", "Lluvia", "Neblina"),
                EstadoCalzada = faker.PickRandom("Seca", "Mojada", "Con barro", "Con baches"),
                EstadoCaso = faker.PickRandom(
                    "en_investigacion", "conciliado", "remitido_fiscalia", "cerrado"),
                OficialAsignadoId = faker.PickRandom(oficiales),
                Observaciones = faker.PickRandom(
                    "Uno de los conductores no respetó la señal de pare.",
                    "Exceso de velocidad en zona urbana.",
                    "Conductor distraído por el uso del celular.",
                    "Pérdida de control del vehículo en calzada mojada.",
                    "Peatón cruzó fuera del paso cebra."),
                CreatedAt = DateTime.UtcNow
            });
        }

        await context.Accidentes.AddRangeAsync(accidentes);
        await context.SaveChangesAsync();

        return accidentes.Count;
    }
}

