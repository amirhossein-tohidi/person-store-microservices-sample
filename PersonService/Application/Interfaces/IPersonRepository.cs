using PersonService.Api.Domain;

namespace PersonService.Api.Application.Interfaces;

public interface IPersonRepository
{
    Task<Person?> GetByNationalCodeAsync(string nationalCode, CancellationToken ct);
    Task<IReadOnlyList<Person>> GetAllPersonsAsync(CancellationToken ct);
}