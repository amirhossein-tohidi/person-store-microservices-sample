using Microsoft.EntityFrameworkCore;
using StoreService.Api.Application.Interfaces.Repositories;
using StoreService.Api.Domain.Entities;
using StoreService.Api.Domain.ValueObjects;
using StoreService.Api.Infrastructure.Persistence.Repositories.Common;

namespace StoreService.Api.Infrastructure.Persistence.Repositories;

public sealed class InvoiceRepository(StoreDbContext context)
    : BaseRepository<Invoice>(context), IInvoiceRepository
{
    public override async Task<Invoice?> GetByIdAsync(
        long id,
        CancellationToken ct = default)
    {
        return await _dbSet
            .Include(invoice => invoice.Items)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<Invoice?> GetByCreationTokenAsync(
        CreationToken token,
        CancellationToken ct = default)
    {
        return await _dbSet
            .Include(invoice => invoice.Items)
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.CreationToken.Value == token.Value,
                ct);
    }

    public async Task<bool> ExistsByCreationTokenAsync(
        CreationToken token,
        CancellationToken ct = default)
    {
        return await _dbSet
            .AnyAsync(x => x.CreationToken.Value == token.Value, ct);
    }
}