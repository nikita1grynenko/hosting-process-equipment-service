using HostingProcessEquipmentService.Application.Contracts;
using HostingProcessEquipmentService.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HostingProcessEquipmentService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContractsController : ControllerBase
{
    private readonly IContractService _contractService;

    public ContractsController(IContractService contractService)
    {
        _contractService = contractService;
    }

    [HttpPost("CreateContract")]
    public async Task<IActionResult> CreateContract([FromBody] CreateContractDto dto)
    {
        try
        {
            await _contractService.CreateContractAsync(dto);
            return Ok("Contract created successfully.");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetContracts")]
    public async Task<IActionResult> GetContracts()
    {
        var contracts = await _contractService.GetContractsAsync();
        return Ok(contracts);
    }
}