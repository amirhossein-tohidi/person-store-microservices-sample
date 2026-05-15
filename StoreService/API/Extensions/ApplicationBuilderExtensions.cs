using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using StoreService.Api.Infrastructure.Persistence;

namespace StoreService.Api.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static WebApplication UseOpenApiDocumentation(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment()) return app;
        
        app.MapOpenApi();
        app.MapScalarApiReference();

        return app;
    }
    
    public static async Task<WebApplication> InitializeDatabaseAsync(
        this WebApplication app,
        CancellationToken ct = default)
    {
        await using var scope = app.Services.CreateAsyncScope();

        var db = scope.ServiceProvider.GetRequiredService<StoreDbContext>();

        await db.Database.MigrateAsync(ct);
        await StoreDbSeeder.SeedAsync(db, ct);

        return app;
    }
}