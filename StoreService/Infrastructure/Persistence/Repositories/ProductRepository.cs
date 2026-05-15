using Microsoft.EntityFrameworkCore;
using StoreService.Api.Application.Interfaces.Repositories;
using StoreService.Api.Domain.Entities;
using StoreService.Api.Infrastructure.Persistence.Repositories.Common;

namespace StoreService.Api.Infrastructure.Persistence.Repositories;

public class ProductRepository(StoreDbContext context) 
    : BaseRepository<Product>(context), IProductRepository
{
    public async Task<List<Product>> GetByIdsAsync(
        IEnumerable<long> ids,
        CancellationToken ct = default)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(p => ids.Contains(p.Id))
            .ToListAsync(ct);
    }
    
    public async Task<List<Product>> GetAllAsync(CancellationToken ct = default)
    {
        return await context.Products.ToListAsync(ct);
    }
}