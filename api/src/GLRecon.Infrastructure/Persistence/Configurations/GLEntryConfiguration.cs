using GLRecon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GLRecon.Infrastructure.Persistence.Configurations;

public class GLEntryConfiguration : IEntityTypeConfiguration<GLEntry>
{
    public void Configure(EntityTypeBuilder<GLEntry> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Description).IsRequired().HasMaxLength(500);
        builder.Property(e => e.Reference).HasMaxLength(100);
        builder.Property(e => e.Amount).HasPrecision(18, 2);
        builder.Property(e => e.Category).HasConversion<string>();
        builder.HasIndex(e => e.EngagementId);
    }
}
