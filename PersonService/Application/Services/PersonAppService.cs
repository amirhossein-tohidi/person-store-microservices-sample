using PersonService.Api.Application.Interfaces;
using PersonService.Api.Domain;

namespace PersonService.Api.Application.Services;

public class PersonAppService(IPersonRepository repository)
{
    public async Task<Person?> GetByNationalCode(string nationalCode, CancellationToken ct)
    {
        return await repository.GetByNationalCodeAsync(nationalCode, ct);
    }
    public async Task<IReadOnlyList<Person>> GetAllPersonsAsync(CancellationToken ct)
    {
        return await repository.GetAllPersonsAsync(ct);
    }
}