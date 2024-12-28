using HostingProcessEquipmentService.Application.Contracts;
using HostingProcessEquipmentService.Application.DTOs;
using HostingProcessEquipmentService.Domain.Entities;
using HostingProcessEquipmentService.Infrastructure.Contracts;

namespace HostingProcessEquipmentService.Application.Services;

public class ContractService : IContractService
{
    private readonly IContractRepository _contractRepository;
    private readonly IQueueService _queueService;

    public ContractService(
        IContractRepository contractRepository,
        IQueueService queueService)
    {
        _contractRepository = contractRepository;
        _queueService = queueService;
    }

    public async Task CreateContractAsync(CreateContractDto dto)
    {
        var facility = await _contractRepository.GetFacilityByCodeAsync(dto.FacilityCode);
        if (facility == null)
            throw new InvalidOperationException("Production facility not found.");

        var equipment = await _contractRepository.GetEquipmentByCodeAsync(dto.EquipmentCode);
        if (equipment == null)
            throw new InvalidOperationException("Process equipment not found.");

        var usedArea = await _contractRepository.GetUsedAreaAsync(facility.Id);
        var requiredArea = dto.EquipmentQuantity * equipment.Area;
        if (usedArea + requiredArea > facility.StandardArea)
            throw new InvalidOperationException("Not enough space in the facility.");

        var contract = new EquipmentPlacementContract
        {
            ProductionFacilityId = facility.Id,
            ProcessEquipmentId = equipment.Id,
            EquipmentQuantity = dto.EquipmentQuantity
        };

        await _contractRepository.AddContractAsync(contract);

        // Додаємо контракт у чергу
        var message = $"Contract {contract.Id} created for facility {facility.Name} with equipment {equipment.Name}.";
        await _queueService.EnqueueMessageAsync(message);
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