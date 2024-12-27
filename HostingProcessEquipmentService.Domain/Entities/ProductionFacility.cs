namespace HostingProcessEquipmentService.Domain.Entities;

public class ProductionFacility
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public double StandardArea { get; set; } // Загальна площа, доступна для обладнання

    public ICollection<EquipmentPlacementContract> Contracts { get; set; } = new List<EquipmentPlacementContract>();
}