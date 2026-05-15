using StoreService.Api.Application.Interfaces.Repositories.Common;

namespace StoreService.Api.Infrastructure.Persistence.Repositories.Common;

public class UnitOfWork(StoreDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(
        CancellationToken ct = default)
    {
        return await context.SaveChangesAsync(ct);
    }
}