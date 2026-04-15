using Microsoft.EntityFrameworkCore;
using GimnasioApi.Entidades; 

namespace GimnasioApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Plan> Planes { get; set; }
    public DbSet<ClientePlan> ClientesPlanes { get; set; }
    public DbSet<TipoTelefono> TipoTelefonos { get; set; }
public DbSet<Beneficio> Beneficios { get; set; }
    public DbSet<Telefono> Telefonos { get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Definimos las llaves primarias manualmente para ir sobre seguro
        modelBuilder.Entity<Cliente>().HasKey(x => x.IdCliente);
        modelBuilder.Entity<Plan>().HasKey(x => x.IdPlan);
        modelBuilder.Entity<ClientePlan>().HasKey(x => x.IdClientePlan);
        modelBuilder.Entity<Telefono>().HasKey(t => t.IdTelefono);
        modelBuilder.Entity<Beneficio>().HasKey(t => t.IdBeneficio);
        modelBuilder.Entity<TipoTelefono>().HasKey(t => t.IdTipoTelefono);
modelBuilder.Entity<Telefono>()
    .HasOne(t => t.Cliente)
    .WithMany(c => c.Telefonos)
    .HasForeignKey(t => t.IdCliente);

modelBuilder.Entity<Telefono>()
    .HasOne(t => t.TipoTelefono)
    .WithMany(tt => tt.Telefonos)
    .HasForeignKey(t => t.IdTipoTelefono);

modelBuilder.Entity<ClientePlan>()
    .HasOne(cp => cp.Cliente)
    .WithMany(c => c.ClientesPlanes)
    .HasForeignKey(cp => cp.IdCliente);

modelBuilder.Entity<ClientePlan>()
    .HasOne(cp => cp.Plan)
    .WithMany(p => p.ClientesPlanes)
    .HasForeignKey(cp => cp.IdPlan);

modelBuilder.Entity<Beneficio>()
    .HasOne(b => b.Plan)
    .WithMany(p => p.Beneficios)
    .HasForeignKey(b => b.IdPlan);
        // Configuración para el precio
        modelBuilder.Entity<Plan>().Property(x => x.Precio).HasColumnType("decimal(18,2)");
    }
}