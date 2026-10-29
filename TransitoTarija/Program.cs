using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TransitoTarija.Data;
using TransitoTarija.Interfaces;
using TransitoTarija.Services;
using TransitoTarija.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

var conexionLocal = builder.Configuration.GetConnectionString("TransitoConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(conexionLocal)
);

builder.Services.AddScoped<IOficialService, OficialService>();
builder.Services.AddScoped<IAccidenteService, AccidenteService>();
builder.Services.AddScoped<IVehiculoInvolucradoService, VehiculoInvolucradoService>();
builder.Services.AddScoped<IPersonaInvolucradaService, PersonaInvolucradaService>();
builder.Services.AddScoped<IPeritajeTecnicoService, PeritajeTecnicoService>();
builder.Services.AddScoped<IEstadisticaService, EstadisticaService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

if (args.Contains("--seed-vehiculos"))
{
    if (!app.Environment.IsDevelopment())
        throw new InvalidOperationException("Solo se permite cargar datos en Development.");

    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var cantidad = await VehiculosSeeder.GenerarAsync(context);
    Console.WriteLine($"Vehiculos generados: {cantidad}");
    return;
}

app.Run();
