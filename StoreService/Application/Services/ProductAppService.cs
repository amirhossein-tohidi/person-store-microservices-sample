using StoreService.Api.API.DTOs;
using StoreService.Api.Application.Interfaces.Repositories;
using StoreService.Api.Application.Interfaces.Repositories.Common;
using StoreService.Api.Domain.Entities;

namespace StoreService.Api.Application.Services;

public class ProductAppService(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork)
{
    public async Task<List<ProductResponseDto>> GetAllAsync(CancellationToken ct)
    {
        var products = await productRepository.GetAllAsync(ct);

        return products
            .Select(p => new ProductResponseDto
            (
                Id : p.Id,
                Name : p.Name,
                Price : p.UnitPrice
             ))
            .ToList();
    }

    public async Task<ProductResponseDto?> GetByIdAsync(long id, CancellationToken ct)
    {
        var product = await productRepository.GetByIdAsync(id, ct);

        if (product is null)
            return null;

        return new ProductResponseDto
        (
            Id: product.Id,
            Name: product.Name,
            Price: product.UnitPrice
        );
    }

    public async Task<ProductResponseDto> CreateAsync(
        CreateProductRequestDto request,
        CancellationToken ct)
    {
        var product = new Product(
            request.Name,
            request.Price);

        await productRepository.AddAsync(product, ct);

        await unitOfWork.SaveChangesAsync(ct);

        return new ProductResponseDto
        (
            Id: product.Id,
            Name: product.Name,
            Price: product.UnitPrice
        );
    }
}