namespace PersonService.Api.API.DTOs;

public record PersonResponseDto(
    string NationalCode,
    string FirstName,
    string LastName
);