using FluentAssertions;
using GLRecon.Domain.Entities;

namespace GLRecon.Domain.Tests.Entities;

public class ReconciliationMatchTests
{
    [Fact]
    public void Create_SetsPropertiesCorrectly()
    {
        var reconciliationResultId = Guid.NewGuid();
        var glEntryId = Guid.NewGuid();
        var bankTransactionId = Guid.NewGuid();
        var before = DateTime.UtcNow;

        var match = ReconciliationMatch.Create(reconciliationResultId, glEntryId, bankTransactionId, 0.87m);

        var after = DateTime.UtcNow;
        match.Id.Should().NotBeEmpty();
        match.ReconciliationResultId.Should().Be(reconciliationResultId);
        match.GLEntryId.Should().Be(glEntryId);
        match.BankTransactionId.Should().Be(bankTransactionId);
        match.ConfidenceScore.Should().Be(0.87m);
        match.MatchedAt.Should().BeOnOrAfter(before).And.BeOnOrBefore(after);
    }

    [Fact]
    public void Create_GeneratesUniqueIds()
    {
        var a = ReconciliationMatch.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 1.0m);
        var b = ReconciliationMatch.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 1.0m);

        a.Id.Should().NotBe(b.Id);
    }
}
