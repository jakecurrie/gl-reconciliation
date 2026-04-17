using GLRecon.Domain.Entities;

namespace GLRecon.Domain.Repositories;

public interface IGLEntryRepository
{
    Task<IReadOnlyList<GLEntry>> GetByEngagementIdAsync(Guid engagementId, CancellationToken ct = default);
    Task AddRangeAsync(IEnumerable<GLEntry> entries, CancellationToken ct = default);
}
