using GLRecon.Domain.Entities;
using GLRecon.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GLRecon.Infrastructure.Persistence.Repositories;

public class GLEntryRepository(AppDbContext db) : IGLEntryRepository
{
    public async Task<IReadOnlyList<GLEntry>> GetByEngagementIdAsync(Guid engagementId, CancellationToken ct = default) =>
        await db.GLEntries.Where(e => e.EngagementId == engagementId).ToListAsync(ct);

    public async Task AddRangeAsync(IEnumerable<GLEntry> entries, CancellationToken ct = default)
    {
        await db.GLEntries.AddRangeAsync(entries, ct);
        await db.SaveChangesAsync(ct);
    }
}
