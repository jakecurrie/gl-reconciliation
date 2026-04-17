using System.Text.Json;
using Azure.Messaging.ServiceBus;
using GLRecon.Api.Models;

namespace GLRecon.Api.Services;

public class ServiceBusPublisher(ServiceBusClient client) : IServiceBusPublisher
{
    private const string QueueName = "gl-classification-request";

    public async Task PublishAsync(ReconciliationRequestMessage message, CancellationToken ct = default)
    {
        await using var sender = client.CreateSender(QueueName);
        var body = JsonSerializer.Serialize(message);
        await sender.SendMessageAsync(new ServiceBusMessage(body), ct);
    }
}
