using GLRecon.Domain.Enums;

namespace GLRecon.Domain.Entities;

public class ReconciliationResult
{
    public Guid Id { get; private set; }
    public Guid EngagementId { get; private set; }
    public ReconciliationStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    private readonly List<ReconciliationMatch> _matches = [];
    public IReadOnlyList<ReconciliationMatch> Matches => _matches.AsReadOnly();

    private readonly List<Guid> _unmatchedGLEntryIds = [];
    public IReadOnlyList<Guid> UnmatchedGLEntryIds => _unmatchedGLEntryIds.AsReadOnly();

    private readonly List<Guid> _unmatchedBankTransactionIds = [];
    public IReadOnlyList<Guid> UnmatchedBankTransactionIds => _unmatchedBankTransactionIds.AsReadOnly();

    private ReconciliationResult() { }

    public static ReconciliationResult Create(Guid engagementId)
    {
        return new ReconciliationResult
        {
            Id = Guid.NewGuid(),
            EngagementId = engagementId,
            Status = ReconciliationStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void MarkProcessing() => Status = ReconciliationStatus.Processing;

    public void Complete(
        IEnumerable<ReconciliationMatch> matches,
        IEnumerable<Guid> unmatchedGLEntryIds,
        IEnumerable<Guid> unmatchedBankTransactionIds)
    {
        _matches.AddRange(matches);
        _unmatchedGLEntryIds.AddRange(unmatchedGLEntryIds);
        _unmatchedBankTransactionIds.AddRange(unmatchedBankTransactionIds);
        Status = ReconciliationStatus.Completed;
        CompletedAt = DateTime.UtcNow;
    }

    public void MarkFailed()
    {
        Status = ReconciliationStatus.Failed;
        CompletedAt = DateTime.UtcNow;
    }
}
