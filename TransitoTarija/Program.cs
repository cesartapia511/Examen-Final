using Microsoft.EntityFrameworkCore;
using TransitoTarija.Data;
using TransitoTarija.Interfaces;
using TransitoTarija.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var conexionLocal = builder.Configuration.GetConnectionString("TransitoConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(conexionLocal)
);

builder.Services.AddScoped<IVehiculoInvolucradoService, VehiculoInvolucradoService>();
builder.Services.AddScoped<IAccidenteService, AccidenteService>();
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

app.Run();