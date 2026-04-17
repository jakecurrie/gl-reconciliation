using GLRecon.Domain.Enums;

namespace GLRecon.Domain.Entities;

public class Engagement
{
    public Guid Id { get; private set; }
    public string ClientName { get; private set; } = string.Empty;
    public DateOnly PeriodStart { get; private set; }
    public DateOnly PeriodEnd { get; private set; }
    public EngagementStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Engagement() { }

    public static Engagement Create(string clientName, DateOnly periodStart, DateOnly periodEnd)
    {
        return new Engagement
        {
            Id = Guid.NewGuid(),
            ClientName = clientName,
            PeriodStart = periodStart,
            PeriodEnd = periodEnd,
            Status = EngagementStatus.Draft,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void MarkProcessing() => Status = EngagementStatus.Processing;
    public void MarkCompleted() => Status = EngagementStatus.Completed;
    public void MarkFailed() => Status = EngagementStatus.Failed;
}
