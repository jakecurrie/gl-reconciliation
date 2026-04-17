using GLRecon.Domain.Entities;

namespace GLRecon.Domain.Repositories;

public interface IBankTransactionRepository
{
    Task<IReadOnlyList<BankTransaction>> GetByEngagementIdAsync(Guid engagementId, CancellationToken ct = default);
    Task AddRangeAsync(IEnumerable<BankTransaction> transactions, CancellationToken ct = default);
}
