using StoreService.Api.API.Endpoints;
using StoreService.Api.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddExternalServices(builder.Configuration)
    .AddApplicationServices()
    .AddOpenApiDocumentation();

var app = builder.Build();



app.UseOpenApiDocumentation();
app.MapInvoiceEndpoints();
app.MapProductEndpoints();
await app.InitializeDatabaseAsync(app.Lifetime.ApplicationStopping);

app.Run();
