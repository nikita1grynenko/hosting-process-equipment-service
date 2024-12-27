namespace HostingProcessEquipmentService.Application.DTOs;

public class CreateContractDto
{
    public string FacilityCode { get; set; } = string.Empty;
    public string EquipmentCode { get; set; } = string.Empty;
    public int EquipmentQuantity { get; set; }
}