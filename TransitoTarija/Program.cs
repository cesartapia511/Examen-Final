using Microsoft.EntityFrameworkCore;
using TransitoTarija.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var conexionLocal = builder.Configuration.GetConnectionString("TransitoConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(conexionLocal)
);



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
