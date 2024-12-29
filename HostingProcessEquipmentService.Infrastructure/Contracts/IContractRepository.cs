using HostingProcessEquipmentService.Domain.Entities;

namespace HostingProcessEquipmentService.Infrastructure.Contracts;

public interface IContractRepository
{
    Task<ProductionFacility?> GetFacilityByCodeAsync(string code);
    Task<ProcessEquipment?> GetEquipmentByCodeAsync(string code);
    Task<double> GetUsedAreaAsync(int facilityId);
    Task AddContractAsync(EquipmentPlacementContract contract);
    Task<IEnumerable<EquipmentPlacementContract>> GetContractsAsync();
}