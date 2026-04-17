using GLRecon.Domain.Entities;
using GLRecon.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GLRecon.Infrastructure.Persistence.Repositories;

public class ReconciliationRepository(AppDbContext db) : IReconciliationRepository
{
    public Task<ReconciliationResult?> GetByEngagementIdAsync(Guid engagementId, CancellationToken ct = default) =>
        db.ReconciliationResults
            .Include(r => r.Matches)
            .FirstOrDefaultAsync(r => r.EngagementId == engagementId, ct);

    public async Task AddAsync(ReconciliationResult result, CancellationToken ct = default)
    {
        await db.ReconciliationResults.AddAsync(result, ct);
        await db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(ReconciliationResult result, CancellationToken ct = default)
    {
        db.ReconciliationResults.Update(result);
        await db.SaveChangesAsync(ct);
    }
}
