using PersonService.Api.API.DTOs;
using PersonService.Api.API.Validators;
using PersonService.Api.Application.Services;

namespace PersonService.Api.API.Endpoints;

public static class PersonEndpoints
{
    public static void MapPersonEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/persons");

        group.MapGet("/{nationalCode}", GetPersonByNationalCodeAsync);

        group.MapGet("/", GetAllPersonsAsync);
    }

    private static async Task<IResult> GetPersonByNationalCodeAsync(
        string nationalCode,
        PersonAppService service,
        CancellationToken ct)
    {
        if (!NationalCodeValidator.IsValid(nationalCode))
            return Results.BadRequest("Invalid national code");

        var person = await service.GetByNationalCode(nationalCode, ct);

        if (person is null)
            return Results.NotFound();

        var dto = new PersonResponseDto(
            person.NationalCode,
            person.FirstName,
            person.LastName
        );

        return Results.Ok(dto);
    }

    private static async Task<IResult> GetAllPersonsAsync(
        PersonAppService service, CancellationToken ct)
    {
        var persons = await service.GetAllPersonsAsync(ct);

        var dtos = persons.Select(person => new PersonResponseDto(
            person.NationalCode,
            person.FirstName,
            person.LastName
        ));

        return Results.Ok(dtos);
    }
}