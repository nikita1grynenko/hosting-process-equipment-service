using HostingProcessEquipmentService.Api.Controllers;
using HostingProcessEquipmentService.Application.Contracts;
using HostingProcessEquipmentService.Application.DTOs;
using HostingProcessEquipmentService.Application.Services;
using HostingProcessEquipmentService.Domain.Entities;
using HostingProcessEquipmentService.Infrastructure.Contracts;

namespace HostingProcessEquipmentService.Api.Tests;

using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ContractsControllerTests
{
    [Fact]
    public async Task CreateContract_ReturnsOk_WhenValidRequest()
    {
        var mockService = new Mock<IContractService>();
        var controller = new ContractsController(mockService.Object);

        var createContractDto = new CreateContractDto
        {
            FacilityCode = "F001",
            EquipmentCode = "E001",
            EquipmentQuantity = 5
        };

        var result = await controller.CreateContract(createContractDto);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Contract created successfully.", okResult.Value);
    }

    [Fact]
    public async Task CreateContract_ReturnsBadRequest_WhenInvalidRequest()
    {
        var mockService = new Mock<IContractService>();
        mockService.Setup(s => s.CreateContractAsync(It.IsAny<CreateContractDto>()))
                   .ThrowsAsync(new InvalidOperationException("Not enough space in the facility"));

        var controller = new ContractsController(mockService.Object);

        var createContractDto = new CreateContractDto
        {
            FacilityCode = "F001",
            EquipmentCode = "E001",
            EquipmentQuantity = 50
        };

        var result = await controller.CreateContract(createContractDto);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Not enough space in the facility", badRequestResult.Value);
    }

    [Fact]
    public async Task GetContracts_ReturnsContractsList()
    {
        var mockService = new Mock<IContractService>();
        mockService.Setup(s => s.GetContractsAsync())
                   .ReturnsAsync(new List<ContractDto>
                   {
                       new ContractDto
                       {
                           FacilityName = "Facility 1",
                           EquipmentName = "Equipment 1",
                           EquipmentQuantity = 5
                       }
                   });

        var controller = new ContractsController(mockService.Object);

        var result = await controller.GetContracts();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var contracts = Assert.IsAssignableFrom<IEnumerable<ContractDto>>(okResult.Value);
        Assert.Single(contracts);
    }
    
    [Fact]
    public async Task CreateContract_ThrowsException_WhenNotEnoughSpace()
    {
        var mockRepository = new Mock<IContractRepository>();
        mockRepository.Setup(r => r.GetFacilityByCodeAsync("F001"))
            .ReturnsAsync(new ProductionFacility
            {
                Id = 1,
                Name = "Facility 1",
                StandardArea = 100
            });

        mockRepository.Setup(r => r.GetEquipmentByCodeAsync("E001"))
            .ReturnsAsync(new ProcessEquipment
            {
                Id = 1,
                Name = "Equipment 1",
                Area = 50
            });

        mockRepository.Setup(r => r.GetUsedAreaAsync(1))
            .ReturnsAsync(60); 

        var service = new ContractService(mockRepository.Object, Mock.Of<IQueueService>());

        var createContractDto = new CreateContractDto
        {
            FacilityCode = "F001",
            EquipmentCode = "E001",
            EquipmentQuantity = 1
        };

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => service.CreateContractAsync(createContractDto));
    }
}