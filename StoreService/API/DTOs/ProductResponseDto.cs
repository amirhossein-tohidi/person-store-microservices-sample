namespace StoreService.Api.API.DTOs;

public sealed record  ProductResponseDto(
    long Id,
    string Name,
    decimal Price
);