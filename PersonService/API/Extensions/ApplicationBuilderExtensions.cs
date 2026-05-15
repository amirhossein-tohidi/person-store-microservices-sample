using Microsoft.EntityFrameworkCore;
using PersonService.Api.Infrastructure.Persistence;
using Scalar.AspNetCore;

namespace PersonService.Api.API.Extensions;

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

        var db = scope.ServiceProvider.GetRequiredService<PersonDbContext>();

        await db.Database.MigrateAsync(ct);
        await PersonDbSeeder.SeedAsync(db, ct);

        return app;
    }
}