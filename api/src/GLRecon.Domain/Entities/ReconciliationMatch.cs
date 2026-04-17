namespace GLRecon.Domain.Entities;

public class ReconciliationMatch
{
    public Guid Id { get; private set; }
    public Guid ReconciliationResultId { get; private set; }
    public Guid GLEntryId { get; private set; }
    public Guid BankTransactionId { get; private set; }
    public decimal ConfidenceScore { get; private set; }
    public DateTime MatchedAt { get; private set; }

    private ReconciliationMatch() { }

    public static ReconciliationMatch Create(
        Guid reconciliationResultId,
        Guid glEntryId,
        Guid bankTransactionId,
        decimal confidenceScore)
    {
        return new ReconciliationMatch
        {
            Id = Guid.NewGuid(),
            ReconciliationResultId = reconciliationResultId,
            GLEntryId = glEntryId,
            BankTransactionId = bankTransactionId,
            ConfidenceScore = confidenceScore,
            MatchedAt = DateTime.UtcNow
        };
    }
}
