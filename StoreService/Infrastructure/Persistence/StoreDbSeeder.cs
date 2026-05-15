using Microsoft.EntityFrameworkCore;
using StoreService.Api.Domain.Entities;

namespace StoreService.Api.Infrastructure.Persistence;

public static class StoreDbSeeder
{
    public static async Task SeedAsync(StoreDbContext context, CancellationToken ct)
    {
        if (await context.Products.AnyAsync(ct))
            return;

        var products = new List<Product>
        {
            new Product("Laptop", 2500),
            new Product("Smartphone", 1200),
            new Product("Tablet", 800),
            new Product("Monitor", 400),
            new Product("Headphones", 150)
        };

        await context.Products.AddRangeAsync(products, ct);

        await context.SaveChangesAsync(ct);
    }
}