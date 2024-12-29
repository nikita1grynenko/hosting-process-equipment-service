using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using HostingProcessEquipmentService.Application.Configuration;
using HostingProcessEquipmentService.Application.Contracts;
using Microsoft.Extensions.Options;

namespace HostingProcessEquipmentService.Application.Services;

public class QueueService : IQueueService
{
    private readonly QueueClient _queueClient;

    public QueueService(IOptions<AzureQueueSettings> settings)
    {
        _queueClient = new QueueClient(settings.Value.ConnectionString, settings.Value.QueueName);
        _queueClient.CreateIfNotExists(); 
    }

    public async Task EnqueueMessageAsync(string message)
    {
        if (!string.IsNullOrEmpty(message))
        {
            await _queueClient.SendMessageAsync(message);
        }
    }

    public async Task<string?> DequeueMessageAsync(CancellationToken cancellationToken)
    {
        QueueMessage[] messages = await _queueClient.ReceiveMessagesAsync(1, null, cancellationToken);
        if (messages.Length > 0)
        {
            string message = messages[0].MessageText;
            await _queueClient.DeleteMessageAsync(messages[0].MessageId, messages[0].PopReceipt, cancellationToken);
            return message;
        }
        return null;
    }
}