using StoreService.Api.API.DTOs;
using StoreService.Api.Application.Services;
using StoreService.Api.Domain.ValueObjects;

namespace StoreService.Api.API.Endpoints;

public static class InvoiceEndpoints
{
    public static void MapInvoiceEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/invoices");

        group.MapPost("/", CreateInvoice);
        group.MapGet("/generate-token", GenerateCreationToken);
    }

    private static async Task<IResult> CreateInvoice(
        CreateInvoiceRequestDto request,
        InvoiceAppService service,
        CancellationToken ct)
    {
        var result = await service.CreateInvoiceAsync(request, ct);

        return Results.Ok(result);
    }
    
    private static IResult GenerateCreationToken()
    {
        var token = CreationToken.New();

        return Results.Ok(new
        {
            creationToken = token.Value
        });
    }
}