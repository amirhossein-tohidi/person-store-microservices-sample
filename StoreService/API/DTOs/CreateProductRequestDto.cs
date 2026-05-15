namespace StoreService.Api.API.DTOs;

public sealed record  CreateProductRequestDto(
    string Name,
    decimal Price
);