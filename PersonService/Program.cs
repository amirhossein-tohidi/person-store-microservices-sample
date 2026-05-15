using PersonService.Api.API.Endpoints;
using PersonService.Api.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplicationServices()
    .AddOpenApiDocumentation();

var app = builder.Build();

app.UseOpenApiDocumentation();
app.MapPersonEndpoints();
await app.InitializeDatabaseAsync(app.Lifetime.ApplicationStopping);

app.Run();


