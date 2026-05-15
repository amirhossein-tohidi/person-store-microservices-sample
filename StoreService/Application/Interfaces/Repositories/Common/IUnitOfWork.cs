namespace StoreService.Api.Application.Interfaces.Repositories.Common;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}