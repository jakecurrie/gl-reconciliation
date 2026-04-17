using GLRecon.Domain.Entities;

namespace GLRecon.Api.Models;

public static class CsvParser
{
    public static async Task<IEnumerable<GLEntry>> ParseGLEntries(Guid engagementId, IFormFile file)
    {
        var entries = new List<GLEntry>();
        using var reader = new StreamReader(file.OpenReadStream());
        await reader.ReadLineAsync(); // skip header

        string? line;
        while ((line = await reader.ReadLineAsync()) is not null)
        {
            var cols = line.Split(',');
            if (cols.Length < 4) continue;

            entries.Add(GLEntry.Create(
                engagementId,
                DateOnly.Parse(cols[0].Trim()),
                cols[1].Trim(),
                decimal.Parse(cols[2].Trim()),
                cols[3].Trim()));
        }

        return entries;
    }

    public static async Task<IEnumerable<BankTransaction>> ParseBankTransactions(Guid engagementId, IFormFile file)
    {
        var transactions = new List<BankTransaction>();
        using var reader = new StreamReader(file.OpenReadStream());
        await reader.ReadLineAsync(); // skip header

        string? line;
        while ((line = await reader.ReadLineAsync()) is not null)
        {
            var cols = line.Split(',');
            if (cols.Length < 4) continue;

            transactions.Add(BankTransaction.Create(
                engagementId,
                DateOnly.Parse(cols[0].Trim()),
                cols[1].Trim(),
                decimal.Parse(cols[2].Trim()),
                cols[3].Trim()));
        }

        return transactions;
    }
}
