using Azure.Identity;
using Azure.Messaging.ServiceBus;
using GLRecon.Api.Endpoints;
using GLRecon.Api.Services;
using GLRecon.Domain.Repositories;
using GLRecon.Infrastructure.Persistence;
using GLRecon.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var keyVaultUri = new Uri(builder.Configuration["KeyVault:Uri"]
    ?? throw new InvalidOperationException("KeyVault:Uri is not configured"));

builder.Configuration.AddAzureKeyVault(keyVaultUri, new DefaultAzureCredential());

var connectionString = builder.Configuration.GetConnectionString("Postgres")
    ?? throw new InvalidOperationException("Connection string 'Postgres' is not configured");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IEngagementRepository, EngagementRepository>();
builder.Services.AddScoped<IGLEntryRepository, GLEntryRepository>();
builder.Services.AddScoped<IBankTransactionRepository, BankTransactionRepository>();
builder.Services.AddScoped<IReconciliationRepository, ReconciliationRepository>();

var serviceBusConnection = builder.Configuration["ServiceBus:ConnectionString"]
    ?? throw new InvalidOperationException("ServiceBus:ConnectionString is not configured");

builder.Services.AddSingleton(new ServiceBusClient(serviceBusConnection));
builder.Services.AddSingleton<IServiceBusPublisher, ServiceBusPublisher>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapEngagementEndpoints();

app.Run();
