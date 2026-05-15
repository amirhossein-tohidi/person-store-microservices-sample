namespace StoreService.Api.API.DTOs;

public sealed record CreateInvoiceRequestDto(
    string NationalCode,
    Guid CreationToken,
    List<CreateInvoiceItemDto> Items
);