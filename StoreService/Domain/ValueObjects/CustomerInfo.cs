namespace StoreService.Api.Domain.ValueObjects;

public record CustomerInfo(
    string FirstName,
    string LastName,
    string NationalCode);