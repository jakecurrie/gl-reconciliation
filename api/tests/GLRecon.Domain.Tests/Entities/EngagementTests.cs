using FluentAssertions;
using GLRecon.Domain.Entities;
using GLRecon.Domain.Enums;

namespace GLRecon.Domain.Tests.Entities;

public class EngagementTests
{
    private static readonly DateOnly Start = new(2024, 1, 1);
    private static readonly DateOnly End = new(2024, 3, 31);

    [Fact]
    public void Create_SetsPropertiesCorrectly()
    {
        var before = DateTime.UtcNow;
        var engagement = Engagement.Create("Acme Corp", Start, End);
        var after = DateTime.UtcNow;

        engagement.Id.Should().NotBeEmpty();
        engagement.ClientName.Should().Be("Acme Corp");
        engagement.PeriodStart.Should().Be(Start);
        engagement.PeriodEnd.Should().Be(End);
        engagement.Status.Should().Be(EngagementStatus.Draft);
        engagement.CreatedAt.Should().BeOnOrAfter(before).And.BeOnOrBefore(after);
    }

    [Fact]
    public void Create_GeneratesUniqueIds()
    {
        var a = Engagement.Create("A", Start, End);
        var b = Engagement.Create("B", Start, End);

        a.Id.Should().NotBe(b.Id);
    }

    [Fact]
    public void MarkProcessing_TransitionsFromDraft()
    {
        var engagement = Engagement.Create("Acme", Start, End);

        engagement.MarkProcessing();

        engagement.Status.Should().Be(EngagementStatus.Processing);
    }

    [Fact]
    public void MarkCompleted_SetsCompletedStatus()
    {
        var engagement = Engagement.Create("Acme", Start, End);
        engagement.MarkProcessing();

        engagement.MarkCompleted();

        engagement.Status.Should().Be(EngagementStatus.Completed);
    }

    [Fact]
    public void MarkFailed_SetsFailedStatus()
    {
        var engagement = Engagement.Create("Acme", Start, End);
        engagement.MarkProcessing();

        engagement.MarkFailed();

        engagement.Status.Should().Be(EngagementStatus.Failed);
    }
}
