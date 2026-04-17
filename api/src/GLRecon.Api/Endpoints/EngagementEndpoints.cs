using GLRecon.Api.Models;
using GLRecon.Api.Services;
using GLRecon.Domain.Entities;
using GLRecon.Domain.Repositories;

namespace GLRecon.Api.Endpoints;

public static class EngagementEndpoints
{
    public static IEndpointRouteBuilder MapEngagementEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/engagements");

        group.MapPost("/", CreateEngagement);
        group.MapPost("/{id:guid}/trial-balance", UploadTrialBalance);
        group.MapPost("/{id:guid}/bank-statement", UploadBankStatement);
        group.MapPost("/{id:guid}/reconcile", Reconcile);
        group.MapGet("/{id:guid}/reconciliation", GetReconciliation);

        return app;
    }

    private static async Task<IResult> CreateEngagement(
        CreateEngagementRequest request,
        IEngagementRepository repo,
        CancellationToken ct)
    {
        var engagement = Engagement.Create(request.ClientName, request.PeriodStart, request.PeriodEnd);
        await repo.AddAsync(engagement, ct);
        return Results.Created($"/api/engagements/{engagement.Id}", new EngagementResponse(engagement));
    }

    private static async Task<IResult> UploadTrialBalance(
        Guid id,
        IFormFile file,
        IGLEntryRepository glRepo,
        IEngagementRepository engagementRepo,
        CancellationToken ct)
    {
        var engagement = await engagementRepo.GetByIdAsync(id, ct);
        if (engagement is null) return Results.NotFound();

        var entries = await CsvParser.ParseGLEntries(id, file);
        await glRepo.AddRangeAsync(entries, ct);
        return Results.NoContent();
    }

    private static async Task<IResult> UploadBankStatement(
        Guid id,
        IFormFile file,
        IBankTransactionRepository bankRepo,
        IEngagementRepository engagementRepo,
        CancellationToken ct)
    {
        var engagement = await engagementRepo.GetByIdAsync(id, ct);
        if (engagement is null) return Results.NotFound();

        var transactions = await CsvParser.ParseBankTransactions(id, file);
        await bankRepo.AddRangeAsync(transactions, ct);
        return Results.NoContent();
    }

    private static async Task<IResult> Reconcile(
        Guid id,
        IEngagementRepository engagementRepo,
        IReconciliationRepository reconciliationRepo,
        IServiceBusPublisher publisher,
        CancellationToken ct)
    {
        var engagement = await engagementRepo.GetByIdAsync(id, ct);
        if (engagement is null) return Results.NotFound();

        var result = ReconciliationResult.Create(id);
        await reconciliationRepo.AddAsync(result, ct);

        engagement.MarkProcessing();
        await engagementRepo.UpdateAsync(engagement, ct);

        await publisher.PublishAsync(new ReconciliationRequestMessage(id, result.Id), ct);

        return Results.Accepted($"/api/engagements/{id}/reconciliation");
    }

    private static async Task<IResult> GetReconciliation(
        Guid id,
        IReconciliationRepository reconciliationRepo,
        CancellationToken ct)
    {
        var result = await reconciliationRepo.GetByEngagementIdAsync(id, ct);
        if (result is null) return Results.NotFound();
        return Results.Ok(new ReconciliationResultResponse(result));
    }
}
