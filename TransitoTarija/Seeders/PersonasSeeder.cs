using Bogus;
using Microsoft.EntityFrameworkCore;
using TransitoTarija.Data;
using TransitoTarija.Models;

namespace TransitoTarija.Seeders;

public static class PersonasSeeder
{
    public static async Task<int> GenerarAsync(AppDbContext context)
    {
        const int cantidadObjetivo = 300;

        var cantidadActual = await context.PersonasInvolucradas.CountAsync();

        var cantidadFaltante = cantidadObjetivo - cantidadActual;

        if (cantidadFaltante <= 0)
            return 0;

        var accidentes = await context.Accidentes
            .Select(a => a.Id)
            .ToListAsync();

        var vehiculos = await context.VehiculosInvolucrados
            .Select(v => new { v.Id, v.AccidenteId })
            .ToListAsync();

        if (accidentes.Count == 0 || vehiculos.Count == 0)
        {
            Console.WriteLine("No existen accidentes o vehiculos registrados.");
            return 0;
        }

        var faker = new Faker("es");
        var personas = new List<PersonaInvolucrada>();

        for (int i = 0; i < cantidadFaltante; i++)
        {
            var conVehiculo = faker.Random.Bool(0.8f);
            var vehiculo = conVehiculo ? faker.PickRandom(vehiculos) : null;

            var ci = $"{faker.Random.Int(1000000, 9999999)} {faker.PickRandom("TJ", "CB", "LP", "SC")}";

            var rol = vehiculo == null
                ? faker.PickRandom("peaton", "ciclista")
                : faker.PickRandom("conductor", "conductor", "pasajero");

            var estadoSalud = faker.Random.WeightedRandom(
                new[] { "ileso", "lesionado_leve", "lesionado_grave", "fallecido" },
                new[] { 0.4f, 0.35f, 0.2f, 0.05f });

            var lesionado = estadoSalud != "ileso";

            var persona = new PersonaInvolucrada
            {
                AccidenteId = vehiculo?.AccidenteId ?? faker.PickRandom(accidentes),
                VehiculoId = vehiculo?.Id,
                Nombres = faker.Name.FirstName(),
                Apellidos = $"{faker.Name.LastName()} {faker.Name.LastName()}",
                Ci = ci,
                Telefono = $"+5917{faker.Random.Int(1000000, 9999999)}",
                Rol = rol,
                EstadoSalud = estadoSalud,
                CentroMedicoTraslado = lesionado
                    ? faker.PickRandom(
                        "Hospital Regional San Juan de Dios (HRSJD)",
                        "Hospital Obrero",
                        "Clínica Parroquial")
                    : null,
                DiagnosticoPreliminar = lesionado
                    ? faker.PickRandom(
                        "Policontuso con contusión en hombro derecho",
                        "Trauma craneoencefálico leve",
                        "Fractura de miembro inferior",
                        "Heridas cortantes superficiales")
                    : null,
                CreatedAt = DateTime.UtcNow
            };

            if (rol == "conductor")
            {
                persona.NumeroLicencia = ci.Replace(' ', '-');
                persona.CategoriaLicencia = faker.PickRandom("A", "B", "C", "M");

                var conAlcohol = faker.Random.Bool(0.2f);
                persona.GradoAlcoholemiaGral = conAlcohol
                    ? Math.Round(faker.Random.Decimal(0.1m, 3m), 2)
                    : 0m;
                persona.EstadoAlcoholemia = conAlcohol ? "positivo" : "sobrio";
            }

            personas.Add(persona);
        }

        await context.PersonasInvolucradas.AddRangeAsync(personas);
        await context.SaveChangesAsync();

        return personas.Count;
    }
}

