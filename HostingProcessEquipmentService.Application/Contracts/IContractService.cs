using HostingProcessEquipmentService.Application.DTOs;

namespace HostingProcessEquipmentService.Application.Contracts;

public interface  IContractService
{
    Task CreateContractAsync(CreateContractDto dto);
    Task<IEnumerable<ContractDto>> GetContractsAsync();
}