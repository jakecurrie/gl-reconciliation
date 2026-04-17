namespace GLRecon.Api.Models;

public record CreateEngagementRequest(string ClientName, DateOnly PeriodStart, DateOnly PeriodEnd);

public record ReconciliationRequestMessage(Guid EngagementId, Guid ReconciliationResultId);
