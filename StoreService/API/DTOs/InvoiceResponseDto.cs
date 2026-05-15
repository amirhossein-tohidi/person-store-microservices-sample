namespace StoreService.Api.API.DTOs;

public sealed record InvoiceResponseDto(
    long Id,
    Guid CreationToken,
    string CustomerFirstName,
    string CustomerLastName,
    string CustomerNationalCode,
    List<InvoiceItemResponseDto> Items,
    decimal TotalAmount
);


 