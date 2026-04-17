using GLRecon.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GLRecon.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Engagement> Engagements => Set<Engagement>();
    public DbSet<GLEntry> GLEntries => Set<GLEntry>();
    public DbSet<BankTransaction> BankTransactions => Set<BankTransaction>();
    public DbSet<ReconciliationResult> ReconciliationResults => Set<ReconciliationResult>();
    public DbSet<ReconciliationMatch> ReconciliationMatches => Set<ReconciliationMatch>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
