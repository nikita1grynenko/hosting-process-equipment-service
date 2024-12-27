namespace HostingProcessEquipmentService.Domain.Entities;

public class EquipmentPlacementContract
{
    public int Id { get; set; }
    public int ProductionFacilityId { get; set; }
    public ProductionFacility ProductionFacility { get; set; } = null!;
    public int ProcessEquipmentId { get; set; }
    public ProcessEquipment ProcessEquipment { get; set; } = null!;
    public int EquipmentQuantity { get; set; } // Кількість одиниць обладнання в контракті
}