
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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

var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("Falta configurar Jwt:Key en User Secrets.");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOficialService, OficialService>();
builder.Services.AddScoped<IAccidenteService, AccidenteService>();
builder.Services.AddScoped<IVehiculoInvolucradoService, VehiculoInvolucradoService>();
builder.Services.AddScoped<IPersonaInvolucradaService, PersonaInvolucradaService>();
builder.Services.AddScoped<IPeritajeTecnicoService, PeritajeTecnicoService>();
builder.Services.AddScoped<IEstadisticaService, EstadisticaService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Token obtenido en /api/v1/auth/login"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (args.Contains("--seed") || args.Contains("--seed-vehiculos"))
{
    if (!app.Environment.IsDevelopment())
        throw new InvalidOperationException("Solo se permite cargar datos en Development.");

    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (args.Contains("--seed-vehiculos"))
    {
        var cantidad = await VehiculosSeeder.GenerarAsync(context);
        Console.WriteLine($"Vehiculos generados: {cantidad}");
        return;
    }

    Console.WriteLine($"Oficiales generados: {await OficialesSeeder.GenerarAsync(context)}");
    Console.WriteLine($"Accidentes generados: {await AccidentesSeeder.GenerarAsync(context)}");
    Console.WriteLine($"Vehiculos generados: {await VehiculosSeeder.GenerarAsync(context)}");
    Console.WriteLine($"Personas generadas: {await PersonasSeeder.GenerarAsync(context)}");
    Console.WriteLine($"Peritajes generados: {await PeritajesSeeder.GenerarAsync(context)}");
    return;
}

app.Run();
