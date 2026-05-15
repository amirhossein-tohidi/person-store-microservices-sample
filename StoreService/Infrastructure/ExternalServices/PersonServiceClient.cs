using StoreService.Api.API.DTOs;
using StoreService.Api.Application.Interfaces.ExternalServices;

namespace StoreService.Api.Infrastructure.ExternalServices;

public class PersonServiceClient(HttpClient httpClient) : IPersonServiceClient
{
    public async Task<PersonDto?> GetPersonAsync(string nationalCode, CancellationToken ct)
    {
        var response = await httpClient.GetAsync($"/api/persons/{nationalCode}", ct);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<PersonDto>(cancellationToken: ct);
    }
}