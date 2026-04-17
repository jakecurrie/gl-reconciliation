using GLRecon.Api.Models;

namespace GLRecon.Api.Services;

public interface IServiceBusPublisher
{
    Task PublishAsync(ReconciliationRequestMessage message, CancellationToken ct = default);
}
