using GLRecon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GLRecon.Infrastructure.Persistence.Configurations;

public class ReconciliationResultConfiguration : IEntityTypeConfiguration<ReconciliationResult>
{
    public void Configure(EntityTypeBuilder<ReconciliationResult> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Status).HasConversion<string>();
        builder.HasIndex(e => e.EngagementId);

        builder.HasMany(e => e.Matches)
            .WithOne()
            .HasForeignKey(m => m.ReconciliationResultId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property<List<Guid>>("_unmatchedGLEntryIds")
            .HasField("_unmatchedGLEntryIds")
            .HasColumnName("UnmatchedGLEntryIds")
            .HasColumnType("uuid[]");

        builder.Property<List<Guid>>("_unmatchedBankTransactionIds")
            .HasField("_unmatchedBankTransactionIds")
            .HasColumnName("UnmatchedBankTransactionIds")
            .HasColumnType("uuid[]");
    }
}
