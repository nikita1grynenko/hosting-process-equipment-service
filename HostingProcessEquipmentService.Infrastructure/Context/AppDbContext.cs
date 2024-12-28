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
        
        // Початкові дані для таблиці ProcessEquipment
        modelBuilder.Entity<ProcessEquipment>().HasData(
            new ProcessEquipment { Id = 1, Code = "PE001", Name = "Обладнання 1", Area = 25.0 },
            new ProcessEquipment { Id = 2, Code = "PE002", Name = "Обладнання 2", Area = 30.5 },
            new ProcessEquipment { Id = 3, Code = "PE003", Name = "Обладнання 3", Area = 15.0 }
        );

        // Початкові дані для таблиці ProductionFacility
        modelBuilder.Entity<ProductionFacility>().HasData(
            new ProductionFacility { Id = 1, Code = "PF001", Name = "Завод 1", StandardArea = 1000.0 },
            new ProductionFacility { Id = 2, Code = "PF002", Name = "Завод 2", StandardArea = 1500.0 },
            new ProductionFacility { Id = 3, Code = "PF003", Name = "Завод 3", StandardArea = 2000.0 }
        );

        // Початкові дані для таблиці EquipmentPlacementContract
        modelBuilder.Entity<EquipmentPlacementContract>().HasData(
            new EquipmentPlacementContract { Id = 1, ProductionFacilityId = 1, ProcessEquipmentId = 1, EquipmentQuantity = 10 },
            new EquipmentPlacementContract { Id = 2, ProductionFacilityId = 1, ProcessEquipmentId = 2, EquipmentQuantity = 5 },
            new EquipmentPlacementContract { Id = 3, ProductionFacilityId = 2, ProcessEquipmentId = 3, EquipmentQuantity = 20 },
            new EquipmentPlacementContract { Id = 4, ProductionFacilityId = 3, ProcessEquipmentId = 1, EquipmentQuantity = 8 },
            new EquipmentPlacementContract { Id = 5, ProductionFacilityId = 3, ProcessEquipmentId = 2, EquipmentQuantity = 12 }
        );
    }
}