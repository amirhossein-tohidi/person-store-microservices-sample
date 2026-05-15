using StoreService.Api.Application.Interfaces.Repositories.Common;
using StoreService.Api.Domain.Entities;
using StoreService.Api.Domain.ValueObjects;

namespace StoreService.Api.Application.Interfaces.Repositories;

public interface IInvoiceRepository : IBaseRepository<Invoice>
{
    Task<Invoice?> GetByCreationTokenAsync(CreationToken creationToken, CancellationToken ct);
    Task<bool> ExistsByCreationTokenAsync(CreationToken token, CancellationToken ct);
}