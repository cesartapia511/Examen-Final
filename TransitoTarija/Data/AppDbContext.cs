using Microsoft.EntityFrameworkCore;
using TransitoTarija.Models;

namespace TransitoTarija.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Oficial> Oficiales => Set<Oficial>();
    public DbSet<Accidente> Accidentes => Set<Accidente>();
    public DbSet<VehiculoInvolucrado> VehiculosInvolucrados => Set<VehiculoInvolucrado>();
    public DbSet<PersonaInvolucrada> PersonasInvolucradas => Set<PersonaInvolucrada>();
    public DbSet<PeritajeTecnico> PeritajesTecnicos => Set<PeritajeTecnico>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        
        var oficial = modelBuilder.Entity<Oficial>();

        oficial.ToTable("oficiales");

        oficial.Property(o => o.Id).HasColumnName("id");

        oficial.Property(o => o.Nombres)
            .HasColumnName("nombres").HasMaxLength(100);

        oficial.Property(o => o.Apellidos)
            .HasColumnName("apellidos").HasMaxLength(100);

        oficial.Property(o => o.Grado)
            .HasColumnName("grado").HasMaxLength(50);

        oficial.Property(o => o.NumeroEscalafon)
            .HasColumnName("numero_escalafon").HasMaxLength(30);

        oficial.Property(o => o.Cargo)
            .HasColumnName("cargo").HasMaxLength(80);

        oficial.Property(o => o.Unidad)
            .HasColumnName("unidad").HasMaxLength(80);

        oficial.Property(o => o.Activo).HasColumnName("activo");
        oficial.Property(o => o.CreatedAt).HasColumnName("created_at");

        oficial.HasIndex(o => o.NumeroEscalafon).IsUnique();

        var accidente = modelBuilder.Entity<Accidente>();

        accidente.ToTable("accidentes");

        accidente.Property(a => a.Id).HasColumnName("id");

        accidente.Property(a => a.CodigoCaso)
            .HasColumnName("codigo_caso").HasMaxLength(30);

        accidente.Property(a => a.FechaHora)
            .HasColumnName("fecha_hora");

        accidente.Property(a => a.TipoAccidente)
            .HasColumnName("tipo_accidente").HasMaxLength(60);

        accidente.Property(a => a.Gravedad)
            .HasColumnName("gravedad").HasMaxLength(30);

        accidente.Property(a => a.Distrito)
            .HasColumnName("distrito").HasMaxLength(50);

        accidente.Property(a => a.ZonaBarrio)
            .HasColumnName("zona_barrio").HasMaxLength(100);

        accidente.Property(a => a.DireccionReferencia)
            .HasColumnName("direccion_referencia").HasMaxLength(255);

        accidente.Property(a => a.Latitud)
            .HasColumnName("latitud").HasPrecision(10, 8);

        accidente.Property(a => a.Longitud)
            .HasColumnName("longitud").HasPrecision(11, 8);

        accidente.Property(a => a.CondicionClimatica)
            .HasColumnName("condicion_climatica").HasMaxLength(40);

        accidente.Property(a => a.EstadoCalzada)
            .HasColumnName("estado_calzada").HasMaxLength(40);

        accidente.Property(a => a.EstadoCaso)
            .HasColumnName("estado_caso").HasMaxLength(40);

        accidente.Property(a => a.OficialAsignadoId)
            .HasColumnName("oficial_asignado_id");

        accidente.Property(a => a.Observaciones)
            .HasColumnName("observaciones");

        accidente.Property(a => a.CreatedAt)
            .HasColumnName("created_at");

        accidente.HasIndex(a => a.CodigoCaso).IsUnique();

        accidente.HasOne(a => a.OficialAsignado)
            .WithMany(o => o.AccidentesAsignados)
            .HasForeignKey(a => a.OficialAsignadoId)
            .OnDelete(DeleteBehavior.Restrict);

        var vehiculo = modelBuilder.Entity<VehiculoInvolucrado>();

        vehiculo.ToTable("vehiculos_involucrados");

        vehiculo.Property(v => v.Id).HasColumnName("id");

        vehiculo.Property(v => v.AccidenteId)
            .HasColumnName("accidente_id");

        vehiculo.Property(v => v.Placa)
            .HasColumnName("placa").HasMaxLength(15);

        vehiculo.Property(v => v.Marca)
            .HasColumnName("marca").HasMaxLength(50);

        vehiculo.Property(v => v.Modelo)
            .HasColumnName("modelo").HasMaxLength(50);

        vehiculo.Property(v => v.TipoVehiculo)
            .HasColumnName("tipo_vehiculo").HasMaxLength(50);

        vehiculo.Property(v => v.Color)
            .HasColumnName("color").HasMaxLength(30);

        vehiculo.Property(v => v.NumeroChasis)
            .HasColumnName("numero_chasis").HasMaxLength(50);

        vehiculo.Property(v => v.SoatVigente)
            .HasColumnName("soat_vigente");

        vehiculo.Property(v => v.Aseguradora)
            .HasColumnName("aseguradora").HasMaxLength(80);

        vehiculo.Property(v => v.DescripcionDanios)
            .HasColumnName("descripcion_danios");

        vehiculo.Property(v => v.RemolcadoA)
            .HasColumnName("remolcado_a").HasMaxLength(120);

        vehiculo.Property(v => v.CreatedAt)
            .HasColumnName("created_at");

        vehiculo.HasOne(v => v.Accidente)
            .WithMany(a => a.Vehiculos)
            .HasForeignKey(v => v.AccidenteId)
            .OnDelete(DeleteBehavior.Restrict);

        var persona = modelBuilder.Entity<PersonaInvolucrada>();

        persona.ToTable("personas_involucradas");

        persona.Property(p => p.Id).HasColumnName("id");

        persona.Property(p => p.AccidenteId)
            .HasColumnName("accidente_id");

        persona.Property(p => p.VehiculoId)
            .HasColumnName("vehiculo_id");

        persona.Property(p => p.Nombres)
            .HasColumnName("nombres").HasMaxLength(100);

        persona.Property(p => p.Apellidos)
            .HasColumnName("apellidos").HasMaxLength(100);

        persona.Property(p => p.Ci)
            .HasColumnName("ci").HasMaxLength(25);

        persona.Property(p => p.Telefono)
            .HasColumnName("telefono").HasMaxLength(20);

        persona.Property(p => p.Rol)
            .HasColumnName("rol").HasMaxLength(30);

        persona.Property(p => p.NumeroLicencia)
            .HasColumnName("numero_licencia").HasMaxLength(30);

        persona.Property(p => p.CategoriaLicencia)
            .HasColumnName("categoria_licencia").HasMaxLength(10);

        persona.Property(p => p.GradoAlcoholemiaGral)
            .HasColumnName("grado_alcoholemia_gr_l")
            .HasPrecision(4, 2);

        persona.Property(p => p.EstadoAlcoholemia)
            .HasColumnName("estado_alcoholemia").HasMaxLength(20);

        persona.Property(p => p.EstadoSalud)
            .HasColumnName("estado_salud").HasMaxLength(30);

        persona.Property(p => p.CentroMedicoTraslado)
            .HasColumnName("centro_medico_traslado").HasMaxLength(120);

        persona.Property(p => p.DiagnosticoPreliminar)
            .HasColumnName("diagnostico_preliminar");

        persona.Property(p => p.CreatedAt)
            .HasColumnName("created_at");

        persona.HasOne(p => p.Accidente)
            .WithMany(a => a.Personas)
            .HasForeignKey(p => p.AccidenteId)
            .OnDelete(DeleteBehavior.Restrict);

        persona.HasOne(p => p.Vehiculo)
            .WithMany(v => v.Personas)
            .HasForeignKey(p => p.VehiculoId)
            .OnDelete(DeleteBehavior.Restrict);

        var peritaje = modelBuilder.Entity<PeritajeTecnico>();

        peritaje.ToTable("peritajes_tecnicos");

        peritaje.Property(p => p.Id).HasColumnName("id");

        peritaje.Property(p => p.AccidenteId)
            .HasColumnName("accidente_id");

        peritaje.Property(p => p.PeritoId)
            .HasColumnName("perito_id");

        peritaje.Property(p => p.FechaPeritaje)
            .HasColumnName("fecha_peritaje");

        peritaje.Property(p => p.VelocidadEstimadaImpactoKmh)
            .HasColumnName("velocidad_estimada_impacto_kmh")
            .HasPrecision(5, 2);

        peritaje.Property(p => p.HuellaFrenadoMetros)
            .HasColumnName("huella_frenado_metros")
            .HasPrecision(5, 2);

        peritaje.Property(p => p.FactorDeterminante)
            .HasColumnName("factor_determinante");

        peritaje.Property(p => p.Conclusiones)
            .HasColumnName("conclusiones");

        peritaje.Property(p => p.CroquisUrl)
            .HasColumnName("croquis_url").HasMaxLength(255);

        peritaje.Property(p => p.CreatedAt)
            .HasColumnName("created_at");

        peritaje.HasOne(p => p.Accidente)
            .WithOne(a => a.Peritaje)
            .HasForeignKey<PeritajeTecnico>(p => p.AccidenteId)
            .OnDelete(DeleteBehavior.Restrict);

        peritaje.HasOne(p => p.Perito)
            .WithMany(o => o.Peritajes)
            .HasForeignKey(p => p.PeritoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}