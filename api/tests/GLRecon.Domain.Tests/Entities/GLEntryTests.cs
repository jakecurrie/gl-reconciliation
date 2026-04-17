using FluentAssertions;
using GLRecon.Domain.Entities;
using GLRecon.Domain.Enums;

namespace GLRecon.Domain.Tests.Entities;

public class GLEntryTests
{
    private static readonly Guid EngagementId = Guid.NewGuid();
    private static readonly DateOnly Date = new(2024, 2, 15);

    [Fact]
    public void Create_SetsPropertiesCorrectly()
    {
        var before = DateTime.UtcNow;
        var entry = GLEntry.Create(EngagementId, Date, "Office supplies", 250.00m, "REF-001");
        var after = DateTime.UtcNow;

        entry.Id.Should().NotBeEmpty();
        entry.EngagementId.Should().Be(EngagementId);
        entry.Date.Should().Be(Date);
        entry.Description.Should().Be("Office supplies");
        entry.Amount.Should().Be(250.00m);
        entry.Reference.Should().Be("REF-001");
        entry.Category.Should().Be(GLCategory.Unknown);
        entry.CreatedAt.Should().BeOnOrAfter(before).And.BeOnOrBefore(after);
    }

    [Fact]
    public void Create_WithoutReference_LeavesReferenceNull()
    {
        var entry = GLEntry.Create(EngagementId, Date, "Misc", 100m);

        entry.Reference.Should().BeNull();
    }

    [Fact]
    public void SetCategory_UpdatesCategory()
    {
        var entry = GLEntry.Create(EngagementId, Date, "Revenue item", 1000m);

        entry.SetCategory(GLCategory.Revenue);

        entry.Category.Should().Be(GLCategory.Revenue);
    }

    [Theory]
    [InlineData(GLCategory.Revenue)]
    [InlineData(GLCategory.Expense)]
    [InlineData(GLCategory.Asset)]
    [InlineData(GLCategory.Liability)]
    [InlineData(GLCategory.Equity)]
    public void SetCategory_AcceptsAllCategories(GLCategory category)
    {
        var entry = GLEntry.Create(EngagementId, Date, "Item", 500m);

        entry.SetCategory(category);

        entry.Category.Should().Be(category);
    }
}
