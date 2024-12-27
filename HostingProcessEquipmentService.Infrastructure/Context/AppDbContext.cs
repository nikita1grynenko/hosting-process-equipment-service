using HostingProcessEquipmentService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HostingProcessEquipmentService.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> op)
        : base(op)
    {

    }
    
    public DbSet<ProductionFacility> ProductionFacilities { get; set; } = null!;
    public DbSet<ProcessEquipment> ProcessEquipments { get; set; } = null!;
    public DbSet<EquipmentPlacementContract> EquipmentPlacementContracts { get; set; } = null!;
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductionFacility>().HasIndex(p => p.Code).IsUnique();
        modelBuilder.Entity<ProcessEquipment>().HasIndex(p => p.Code).IsUnique();
    }
}