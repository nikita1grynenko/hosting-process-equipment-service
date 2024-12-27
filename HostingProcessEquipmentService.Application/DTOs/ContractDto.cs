namespace HostingProcessEquipmentService.Application.DTOs;

public class ContractDto
{
    public string FacilityName { get; set; } = string.Empty;
    public string EquipmentName { get; set; } = string.Empty;
    public int EquipmentQuantity { get; set; }
}