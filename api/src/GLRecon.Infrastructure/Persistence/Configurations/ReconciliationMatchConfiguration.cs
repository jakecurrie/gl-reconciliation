using GLRecon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GLRecon.Infrastructure.Persistence.Configurations;

public class ReconciliationMatchConfiguration : IEntityTypeConfiguration<ReconciliationMatch>
{
    public void Configure(EntityTypeBuilder<ReconciliationMatch> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.ConfidenceScore).HasPrecision(5, 4);
    }
}
