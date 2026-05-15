using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StoreService.Api.Domain.Common;

namespace StoreService.Api.Infrastructure.Persistence.Repositories.Common;

public abstract class BaseRepository<TEntity>(StoreDbContext context)
    where TEntity : BaseEntity
{
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public virtual async Task<TEntity?> GetByIdAsync(
        long id,
        CancellationToken ct = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public virtual async Task AddAsync(
        TEntity entity,
        CancellationToken ct = default)
    {
        await _dbSet.AddAsync(entity, ct);
    }

    public virtual void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public virtual void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public virtual async Task<bool> ExistsAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken ct = default)
    {
        return await _dbSet.AnyAsync(predicate, ct);
    }
}