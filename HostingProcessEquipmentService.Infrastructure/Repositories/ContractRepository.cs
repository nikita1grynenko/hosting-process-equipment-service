using HostingProcessEquipmentService.Domain.Entities;
using HostingProcessEquipmentService.Infrastructure.Context;
using HostingProcessEquipmentService.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HostingProcessEquipmentService.Infrastructure.Repositories;

public class ContractRepository : IContractRepository
{
    private readonly AppDbContext _context;

    public ContractRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ProductionFacility?> GetFacilityByCodeAsync(string code)
    {
        return await _context.ProductionFacilities
            .Include(f => f.Contracts)
            .ThenInclude(c => c.ProcessEquipment)
            .FirstOrDefaultAsync(f => f.Code == code);
    }

    public async Task<ProcessEquipment?> GetEquipmentByCodeAsync(string code)
    {
        return await _context.ProcessEquipments.FirstOrDefaultAsync(e => e.Code == code);
    }

    public async Task<double> GetUsedAreaAsync(int facilityId)
    {
        return await _context.EquipmentPlacementContracts
            .Where(c => c.ProductionFacilityId == facilityId)
            .SumAsync(c => c.ProcessEquipment.Area * c.EquipmentQuantity);
    }

    public async Task AddContractAsync(EquipmentPlacementContract contract)
    {
        _context.EquipmentPlacementContracts.Add(contract);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<EquipmentPlacementContract>> GetContractsAsync()
    {
        return await _context.EquipmentPlacementContracts
            .Include(c => c.ProductionFacility)
            .Include(c => c.ProcessEquipment)
            .ToListAsync();
    }
}