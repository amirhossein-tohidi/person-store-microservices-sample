using StoreService.Api.Application.Interfaces.Repositories.Common;
using StoreService.Api.Domain.Entities;

namespace StoreService.Api.Application.Interfaces.Repositories;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<Product?> GetByIdAsync(long id, CancellationToken ct);
    Task<List<Product>> GetAllAsync(CancellationToken ct);
    
}