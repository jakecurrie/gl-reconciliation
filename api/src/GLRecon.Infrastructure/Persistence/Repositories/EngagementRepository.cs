using GLRecon.Domain.Entities;
using GLRecon.Domain.Repositories;

namespace GLRecon.Infrastructure.Persistence.Repositories;

public class EngagementRepository(AppDbContext db) : IEngagementRepository
{
    public Task<Engagement?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        db.Engagements.FindAsync([id], ct).AsTask();

    public async Task AddAsync(Engagement engagement, CancellationToken ct = default)
    {
        await db.Engagements.AddAsync(engagement, ct);
        await db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Engagement engagement, CancellationToken ct = default)
    {
        db.Engagements.Update(engagement);
        await db.SaveChangesAsync(ct);
    }
}
