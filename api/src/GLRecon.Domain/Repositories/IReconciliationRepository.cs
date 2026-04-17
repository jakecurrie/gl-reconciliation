using GLRecon.Domain.Entities;

namespace GLRecon.Domain.Repositories;

public interface IReconciliationRepository
{
    Task<ReconciliationResult?> GetByEngagementIdAsync(Guid engagementId, CancellationToken ct = default);
    Task AddAsync(ReconciliationResult result, CancellationToken ct = default);
    Task UpdateAsync(ReconciliationResult result, CancellationToken ct = default);
}
