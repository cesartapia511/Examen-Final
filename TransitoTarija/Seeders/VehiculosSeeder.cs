
using Bogus;
using Microsoft.EntityFrameworkCore;
using TransitoTarija.Data;
using TransitoTarija.Models;

namespace TransitoTarija.Seeders;

public static class VehiculosSeeder
{
    public static async Task<int> GenerarAsync(AppDbContext context)
    {
        const int cantidadObjetivo = 300;

        var cantidadActual = await context.VehiculosInvolucrados.CountAsync();

        var cantidadFaltante = cantidadObjetivo - cantidadActual;

        if (cantidadFaltante <= 0)
            return 0;

        var accidentes = await context.Accidentes
            .Select(a => a.Id)
            .ToListAsync();

        if (accidentes.Count == 0)
        {
            Console.WriteLine("No existen accidentes registrados.");
            return 0;
        }

        var faker = new Faker("es");
        var vehiculos = new List<VehiculoInvolucrado>();

        for (int i = 0; i < cantidadFaltante; i++)
        {
            var vehiculo = new VehiculoInvolucrado
            {
                AccidenteId = faker.PickRandom(accidentes),
                Placa = $"{faker.Random.Int(1000, 9999)}-{faker.Random.String2(3, "ABCDEFGHIJKLMNOPQRSTUVWXYZ")}",
                Marca = faker.PickRandom("Toyota", "Nissan", "Suzuki", "Hyundai", "Kia", "Ford"),
                Modelo = faker.PickRandom("Corolla", "Hilux", "Sentra", "Swift", "Tucson", "Ranger"),
                TipoVehiculo = faker.PickRandom("Automovil", "Camioneta", "Motocicleta", "Camion"),
                Color = faker.PickRandom("Blanco", "Negro", "Rojo", "Azul", "Plata"),
                NumeroChasis = faker.Random.Replace("#################"),
                SoatVigente = faker.Random.Bool(),
                Aseguradora = faker.PickRandom("Alianza", "Credinform", "Nacional Seguros"),
                DescripcionDanios = faker.PickRandom(
                    "Danios en el parachoques",
                    "Danios laterales",
                    "Rotura de parabrisas",
                    "Danios en la parte trasera"
                ),
                RemolcadoA = faker.PickRandom(
                    "Deposito policial",
                    "Taller mecanico",
                    "No fue remolcado"
                ),
                CreatedAt = DateTime.UtcNow
            };

            vehiculos.Add(vehiculo);
        }

        await context.VehiculosInvolucrados.AddRangeAsync(vehiculos);
        await context.SaveChangesAsync();

        return vehiculos.Count;
    }
}
