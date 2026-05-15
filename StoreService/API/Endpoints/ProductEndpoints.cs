using StoreService.Api.API.DTOs;
using StoreService.Api.Application.Services;

namespace StoreService.Api.API.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/products");

        group.MapGet("/", GetAllProducts);

        group.MapGet("/{id:long}", GetProductById);

        group.MapPost("/", CreateProduct);
    }

    private static async Task<IResult> GetAllProducts(
        ProductAppService service,
        CancellationToken ct)
    {
        var result = await service.GetAllAsync(ct);

        return Results.Ok(result);
    }

    private static async Task<IResult> GetProductById(
        long id,
        ProductAppService service,
        CancellationToken ct)
    {
        var product = await service.GetByIdAsync(id, ct);

        if (product is null)
            return Results.NotFound();

        return Results.Ok(product);
    }

    private static async Task<IResult> CreateProduct(
        CreateProductRequestDto request,
        ProductAppService service,
        CancellationToken ct)
    {
        var result = await service.CreateAsync(request, ct);

        return Results.Ok(result);
    }
}
 