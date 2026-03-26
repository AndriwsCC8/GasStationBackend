using GasStation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Infrastructure.Data;

public class GasStationDbContext : DbContext
{
    public GasStationDbContext(DbContextOptions<GasStationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Fuel> Fuels { get; set; } = null!;
    public DbSet<Inventory> Inventories { get; set; } = null!;
    public DbSet<Reception> Receptions { get; set; } = null!;
    public DbSet<Sale> Sales { get; set; } = null!;
    public DbSet<Closure> Closures { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuraciones de precisión para decimales (Evita advertencias en SQL Server)
        modelBuilder.Entity<Fuel>().Property(f => f.Price).HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Inventory>().Property(i => i.Stock).HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Reception>().Property(r => r.Quantity).HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Sale>().Property(s => s.Quantity).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Sale>().Property(s => s.Price).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Sale>().Property(s => s.Total).HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Closure>().Property(c => c.TotalSales).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Closure>().Property(c => c.Differences).HasColumnType("decimal(18,2)");

        // Evitar múltiples Cierres por día (Índice único por fecha - ignorando hora)
        // Para simplificar asumiendo que la fecha se guarda sin hora.
        modelBuilder.Entity<Closure>()
            .HasIndex(c => c.Date)
            .IsUnique();
    }
}
