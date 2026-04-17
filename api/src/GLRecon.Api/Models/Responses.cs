using GLRecon.Domain.Entities;
using GLRecon.Domain.Enums;

namespace GLRecon.Api.Models;

public record EngagementResponse(
    Guid Id,
    string ClientName,
    DateOnly PeriodStart,
    DateOnly PeriodEnd,
    EngagementStatus Status,
    DateTime CreatedAt)
{
    public EngagementResponse(Engagement e)
        : this(e.Id, e.ClientName, e.PeriodStart, e.PeriodEnd, e.Status, e.CreatedAt) { }
}

public record ReconciliationResultResponse(
    Guid Id,
    Guid EngagementId,
    ReconciliationStatus Status,
    DateTime CreatedAt,
    DateTime? CompletedAt,
    IReadOnlyList<ReconciliationMatchResponse> Matches,
    IReadOnlyList<Guid> UnmatchedGLEntryIds,
    IReadOnlyList<Guid> UnmatchedBankTransactionIds)
{
    public ReconciliationResultResponse(ReconciliationResult r)
        : this(
            r.Id,
            r.EngagementId,
            r.Status,
            r.CreatedAt,
            r.CompletedAt,
            r.Matches.Select(m => new ReconciliationMatchResponse(m)).ToList(),
            r.UnmatchedGLEntryIds,
            r.UnmatchedBankTransactionIds)
    { }
}

public record ReconciliationMatchResponse(
    Guid Id,
    Guid GLEntryId,
    Guid BankTransactionId,
    decimal ConfidenceScore,
    DateTime MatchedAt)
{
    public ReconciliationMatchResponse(ReconciliationMatch m)
        : this(m.Id, m.GLEntryId, m.BankTransactionId, m.ConfidenceScore, m.MatchedAt) { }
}
