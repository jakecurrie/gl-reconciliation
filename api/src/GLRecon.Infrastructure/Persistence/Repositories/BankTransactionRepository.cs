using GLRecon.Domain.Entities;
using GLRecon.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GLRecon.Infrastructure.Persistence.Repositories;

public class BankTransactionRepository(AppDbContext db) : IBankTransactionRepository
{
    public async Task<IReadOnlyList<BankTransaction>> GetByEngagementIdAsync(Guid engagementId, CancellationToken ct = default) =>
        await db.BankTransactions.Where(t => t.EngagementId == engagementId).ToListAsync(ct);

    public async Task AddRangeAsync(IEnumerable<BankTransaction> transactions, CancellationToken ct = default)
    {
        await db.BankTransactions.AddRangeAsync(transactions, ct);
        await db.SaveChangesAsync(ct);
    }
}
