using HostingProcessEquipmentService.Application.Contracts;
using HostingProcessEquipmentService.Application.DTOs;
using HostingProcessEquipmentService.Domain.Entities;
using HostingProcessEquipmentService.Infrastructure.Contracts;

namespace HostingProcessEquipmentService.Application.Services;

public class ContractService : IContractService
{
    private readonly IContractRepository _contractRepository;

    public ContractService(IContractRepository contractRepository)
    {
        _contractRepository = contractRepository;
    }

    public async Task CreateContractAsync(CreateContractDto dto)
    {
        var facility = await _contractRepository.GetFacilityByCodeAsync(dto.FacilityCode);
        if (facility == null)
            throw new InvalidOperationException("Production facility not found.");

        var equipment = await _contractRepository.GetEquipmentByCodeAsync(dto.EquipmentCode);
        if (equipment == null)
            throw new InvalidOperationException("Process equipment not found.");

        // Перевірка площі
        var usedArea = await _contractRepository.GetUsedAreaAsync(facility.Id);
        var requiredArea = dto.EquipmentQuantity * equipment.Area;
        if (usedArea + requiredArea > facility.StandardArea)
            throw new InvalidOperationException("Not enough space in the facility.");

        // Створення контракту
        var contract = new EquipmentPlacementContract
        {
            ProductionFacilityId = facility.Id,
            ProcessEquipmentId = equipment.Id,
            EquipmentQuantity = dto.EquipmentQuantity
        };

        await _contractRepository.AddContractAsync(contract);
    }

    public async Task<IEnumerable<ContractDto>> GetContractsAsync()
    {
        var contracts = await _contractRepository.GetContractsAsync();

        return contracts.Select(c => new ContractDto
        {
            FacilityName = c.ProductionFacility.Name,
            EquipmentName = c.ProcessEquipment.Name,
            EquipmentQuantity = c.EquipmentQuantity
        });
    }
}