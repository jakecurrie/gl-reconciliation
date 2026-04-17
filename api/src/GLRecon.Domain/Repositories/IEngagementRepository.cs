using GLRecon.Domain.Entities;

namespace GLRecon.Domain.Repositories;

public interface IEngagementRepository
{
    Task<Engagement?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(Engagement engagement, CancellationToken ct = default);
    Task UpdateAsync(Engagement engagement, CancellationToken ct = default);
}
