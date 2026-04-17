namespace GLRecon.Domain.Entities;

public class BankTransaction
{
    public Guid Id { get; private set; }
    public Guid EngagementId { get; private set; }
    public DateOnly Date { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public decimal Amount { get; private set; }
    public string? Reference { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private BankTransaction() { }

    public static BankTransaction Create(
        Guid engagementId,
        DateOnly date,
        string description,
        decimal amount,
        string? reference = null)
    {
        return new BankTransaction
        {
            Id = Guid.NewGuid(),
            EngagementId = engagementId,
            Date = date,
            Description = description,
            Amount = amount,
            Reference = reference,
            CreatedAt = DateTime.UtcNow
        };
    }
}
