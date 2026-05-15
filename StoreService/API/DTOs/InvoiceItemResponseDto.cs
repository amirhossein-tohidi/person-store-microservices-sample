namespace StoreService.Api.API.DTOs;

public sealed record InvoiceItemResponseDto(
    long ProductId,
    string ProductName,
    decimal UnitPrice,
    int Quantity,
    decimal TotalPrice
);
