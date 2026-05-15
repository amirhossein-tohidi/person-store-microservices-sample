using Microsoft.EntityFrameworkCore;
using PersonService.Api.Application.Interfaces;
using PersonService.Api.Domain;

namespace PersonService.Api.Infrastructure.Persistence.Repositories;

public class PersonRepository(PersonDbContext context) : IPersonRepository
{
    public async Task<Person?> GetByNationalCodeAsync(string nationalCode, CancellationToken ct)
    {
        return await context.Persons
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.NationalCode == nationalCode, cancellationToken: ct);
    }

    public async Task<IReadOnlyList<Person>> GetAllPersonsAsync(CancellationToken ct)
    {
        return await context.Persons
            .AsNoTracking()
            .ToListAsync(ct);
    }
}