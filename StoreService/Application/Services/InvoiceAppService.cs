using StoreService.Api.API.DTOs;
using StoreService.Api.Application.Interfaces.ExternalServices;
using StoreService.Api.Application.Interfaces.Repositories;
using StoreService.Api.Application.Interfaces.Repositories.Common;
using StoreService.Api.Domain.Entities;
using StoreService.Api.Domain.ValueObjects;

namespace StoreService.Api.Application.Services;

public class InvoiceAppService(
    IInvoiceRepository invoiceRepository,
    IProductRepository productRepository,
    IPersonServiceClient personServiceClient,
    IUnitOfWork unitOfWork)
{
    public async Task<InvoiceResponseDto> CreateInvoiceAsync(CreateInvoiceRequestDto request, CancellationToken ct)
    {
        // 1. Validate input
        if (request == null) throw new ArgumentNullException(nameof(request));
        if (request.Items == null || !request.Items.Any())
            throw new ArgumentException("Invoice must contain at least one item.", nameof(request.Items));
        if (request.CreationToken == Guid.Empty)
            throw new ArgumentException("CreationToken is required.", nameof(request.CreationToken));
        if (string.IsNullOrWhiteSpace(request.NationalCode))
            throw new ArgumentException("NationalCode is required.", nameof(request.NationalCode));

        var creationToken = CreationToken.From(request.CreationToken);

        // 2. Idempotency check
        var existingInvoice = await invoiceRepository.GetByCreationTokenAsync(creationToken, ct);
        if (existingInvoice != null)
        {
            return MapToDto(existingInvoice);
        }

        // 3. Get Customer info from PersonService
        var person = await personServiceClient.GetPersonAsync(request.NationalCode, ct);
        if (person == null)
            throw new InvalidOperationException($"Customer with NationalCode {request.NationalCode} not found.");

        var customerInfo = new CustomerInfo(person.FirstName, person.LastName, person.NationalCode);

        // 4. Create Invoice domain entity
        var invoice = new Invoice(customerInfo, creationToken);

        // 5. Add Items
        foreach (var itemDto in request.Items)
        {
            var product = await productRepository.GetByIdAsync(itemDto.ProductId, ct);
            if (product == null)
                throw new InvalidOperationException($"Product with Id {itemDto.ProductId} not found.");

            var invoiceItem = new InvoiceItem(
                product.Id,
                product.Name,
                product.UnitPrice,
                itemDto.Quantity);

            invoice.AddItem(invoiceItem);
        }

        // 6. Validate Invoice business rules (optional)

        if (!invoice.HasItems)
            throw new InvalidOperationException("Invoice must have at least one item.");

        // 7. Save
        await invoiceRepository.AddAsync(invoice, ct);
        await unitOfWork.SaveChangesAsync(ct);

        // 8. Return DTO
        return MapToDto(invoice);
    }

    private static InvoiceResponseDto MapToDto(Invoice invoice)
    {
        return new InvoiceResponseDto
        (
            Id : invoice.Id,
            CreationToken : invoice.CreationToken.Value,
            CustomerFirstName : invoice.CustomerSnapshot.FirstName,
            CustomerLastName : invoice.CustomerSnapshot.LastName,
            CustomerNationalCode : invoice.CustomerSnapshot.NationalCode,
            Items : invoice.Items.Select(i => new InvoiceItemResponseDto
            (
                ProductId : i.ProductId,
                ProductName : i.ProductNameSnapshot,
                UnitPrice : i.UnitPriceSnapshot,
                Quantity : i.Quantity,
                TotalPrice : i.TotalPrice
            )).ToList(),
            TotalAmount : invoice.TotalAmount
            );
    }
}