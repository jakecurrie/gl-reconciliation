using GLRecon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GLRecon.Infrastructure.Persistence.Configurations;

public class EngagementConfiguration : IEntityTypeConfiguration<Engagement>
{
    public void Configure(EntityTypeBuilder<Engagement> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.ClientName).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Status).HasConversion<string>();
    }
}
