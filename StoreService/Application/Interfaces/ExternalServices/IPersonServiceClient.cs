using StoreService.Api.API.DTOs;

namespace StoreService.Api.Application.Interfaces.ExternalServices;

public interface IPersonServiceClient
{
    Task<PersonDto?> GetPersonAsync(string nationalCode, CancellationToken ct);
}