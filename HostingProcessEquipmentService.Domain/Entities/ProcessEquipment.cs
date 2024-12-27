namespace HostingProcessEquipmentService.Domain.Entities;

public class ProcessEquipment
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public double Area { get; set; } // Площа, необхідна для одного обладнання
}