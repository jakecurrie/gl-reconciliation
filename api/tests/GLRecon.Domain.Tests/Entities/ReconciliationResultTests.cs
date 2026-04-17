using FluentAssertions;
using GLRecon.Domain.Entities;
using GLRecon.Domain.Enums;

namespace GLRecon.Domain.Tests.Entities;

public class ReconciliationResultTests
{
    private static readonly Guid EngagementId = Guid.NewGuid();

    [Fact]
    public void Create_SetsPropertiesCorrectly()
    {
        var before = DateTime.UtcNow;
        var result = ReconciliationResult.Create(EngagementId);
        var after = DateTime.UtcNow;

        result.Id.Should().NotBeEmpty();
        result.EngagementId.Should().Be(EngagementId);
        result.Status.Should().Be(ReconciliationStatus.Pending);
        result.CreatedAt.Should().BeOnOrAfter(before).And.BeOnOrBefore(after);
        result.CompletedAt.Should().BeNull();
        result.Matches.Should().BeEmpty();
        result.UnmatchedGLEntryIds.Should().BeEmpty();
        result.UnmatchedBankTransactionIds.Should().BeEmpty();
    }

    [Fact]
    public void MarkProcessing_SetsProcessingStatus()
    {
        var result = ReconciliationResult.Create(EngagementId);

        result.MarkProcessing();

        result.Status.Should().Be(ReconciliationStatus.Processing);
    }

    [Fact]
    public void Complete_SetsCompletedStatusAndTimestamp()
    {
        var result = ReconciliationResult.Create(EngagementId);
        var before = DateTime.UtcNow;

        result.Complete([], [], []);

        var after = DateTime.UtcNow;
        result.Status.Should().Be(ReconciliationStatus.Completed);
        result.CompletedAt.Should().BeOnOrAfter(before).And.BeOnOrBefore(after);
    }

    [Fact]
    public void Complete_PopulatesMatchesAndUnmatched()
    {
        var result = ReconciliationResult.Create(EngagementId);
        var match = ReconciliationMatch.Create(result.Id, Guid.NewGuid(), Guid.NewGuid(), 0.95m);
        var unmatchedGL = Guid.NewGuid();
        var unmatchedBank = Guid.NewGuid();

        result.Complete([match], [unmatchedGL], [unmatchedBank]);

        result.Matches.Should().ContainSingle().Which.Should().Be(match);
        result.UnmatchedGLEntryIds.Should().ContainSingle().Which.Should().Be(unmatchedGL);
        result.UnmatchedBankTransactionIds.Should().ContainSingle().Which.Should().Be(unmatchedBank);
    }

    [Fact]
    public void Complete_WithMultipleMatches_PopulatesAll()
    {
        var result = ReconciliationResult.Create(EngagementId);
        var matches = Enumerable.Range(0, 3)
            .Select(_ => ReconciliationMatch.Create(result.Id, Guid.NewGuid(), Guid.NewGuid(), 0.9m))
            .ToList();

        result.Complete(matches, [], []);

        result.Matches.Should().HaveCount(3);
    }

    [Fact]
    public void MarkFailed_SetsFailedStatusAndTimestamp()
    {
        var result = ReconciliationResult.Create(EngagementId);
        var before = DateTime.UtcNow;

        result.MarkFailed();

        var after = DateTime.UtcNow;
        result.Status.Should().Be(ReconciliationStatus.Failed);
        result.CompletedAt.Should().BeOnOrAfter(before).And.BeOnOrBefore(after);
    }
}
