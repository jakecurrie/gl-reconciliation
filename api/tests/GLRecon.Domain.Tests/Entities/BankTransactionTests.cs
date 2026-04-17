using FluentAssertions;
using GLRecon.Domain.Entities;

namespace GLRecon.Domain.Tests.Entities;

public class BankTransactionTests
{
    private static readonly Guid EngagementId = Guid.NewGuid();
    private static readonly DateOnly Date = new(2024, 2, 20);

    [Fact]
    public void Create_SetsPropertiesCorrectly()
    {
        var before = DateTime.UtcNow;
        var tx = BankTransaction.Create(EngagementId, Date, "Payment received", 500.00m, "TXN-123");
        var after = DateTime.UtcNow;

        tx.Id.Should().NotBeEmpty();
        tx.EngagementId.Should().Be(EngagementId);
        tx.Date.Should().Be(Date);
        tx.Description.Should().Be("Payment received");
        tx.Amount.Should().Be(500.00m);
        tx.Reference.Should().Be("TXN-123");
        tx.CreatedAt.Should().BeOnOrAfter(before).And.BeOnOrBefore(after);
    }

    [Fact]
    public void Create_WithoutReference_LeavesReferenceNull()
    {
        var tx = BankTransaction.Create(EngagementId, Date, "ATM withdrawal", -100m);

        tx.Reference.Should().BeNull();
    }

    [Fact]
    public void Create_GeneratesUniqueIds()
    {
        var a = BankTransaction.Create(EngagementId, Date, "A", 10m);
        var b = BankTransaction.Create(EngagementId, Date, "B", 20m);

        a.Id.Should().NotBe(b.Id);
    }

    [Fact]
    public void Create_AllowsNegativeAmounts()
    {
        var tx = BankTransaction.Create(EngagementId, Date, "Debit", -250.75m);

        tx.Amount.Should().Be(-250.75m);
    }
}
