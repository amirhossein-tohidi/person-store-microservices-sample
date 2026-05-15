namespace StoreService.Api.API.DTOs;

public sealed record CreateInvoiceItemDto(
    long ProductId,
    int Quantity
);