using Microsoft.EntityFrameworkCore;
using StoreService.Api.Application.Interfaces.ExternalServices;
using StoreService.Api.Application.Interfaces.Repositories;
using StoreService.Api.Application.Interfaces.Repositories.Common;
using StoreService.Api.Application.Services;
using StoreService.Api.Infrastructure.ExternalServices;
using StoreService.Api.Infrastructure.Persistence;
using StoreService.Api.Infrastructure.Persistence.Repositories;
using StoreService.Api.Infrastructure.Persistence.Repositories.Common;

namespace StoreService.Api.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<InvoiceAppService>();
        services.AddScoped<ProductAppService>();
        
        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StoreDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }

    public static IServiceCollection AddExternalServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpClient<IPersonServiceClient, PersonServiceClient>((sp, client) =>
        {
            var baseUrl = configuration["ExternalServices:PersonServiceBaseUrl"];
            client.BaseAddress = new Uri(baseUrl!);
        });

        return services;
    }
    
    public static IServiceCollection AddOpenApiDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddOpenApi();

        return services;
    }
}