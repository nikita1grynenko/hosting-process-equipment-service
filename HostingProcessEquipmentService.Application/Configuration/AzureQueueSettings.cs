﻿namespace HostingProcessEquipmentService.Application.Configuration;

public class AzureQueueSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string QueueName { get; set; } = string.Empty;
}