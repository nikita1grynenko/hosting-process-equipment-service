namespace HostingProcessEquipmentService.Application.Contracts;

public interface IQueueService
{
    Task EnqueueMessageAsync(string message);
    Task<string?> DequeueMessageAsync(CancellationToken cancellationToken);
}