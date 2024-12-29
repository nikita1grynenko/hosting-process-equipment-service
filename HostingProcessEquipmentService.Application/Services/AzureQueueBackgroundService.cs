using HostingProcessEquipmentService.Application.Contracts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HostingProcessEquipmentService.Application.Services;

public class AzureQueueBackgroundService : BackgroundService
{
    private readonly IQueueService _queueService;
    private readonly ILogger<AzureQueueBackgroundService> _logger;

    public AzureQueueBackgroundService(IQueueService queueService, ILogger<AzureQueueBackgroundService> logger)
    {
        _queueService = queueService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Azure Queue Background Service started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var message = await _queueService.DequeueMessageAsync(stoppingToken);
                if (message != null)
                {
                    _logger.LogInformation($"Processing message: {message}");
                    await Task.Delay(1000, stoppingToken);
                }
                else
                {
                    await Task.Delay(5000, stoppingToken); 
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message.");
            }
        }

        _logger.LogInformation("Azure Queue Background Service stopped.");
    }
}
