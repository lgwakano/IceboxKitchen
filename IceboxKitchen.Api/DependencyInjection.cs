using IceboxKitchen.Api.Common.Errors;
using IceboxKitchen.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace IceboxKitchen.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, IceboxKitchenProblemDetailsFactory>();
        services.AddMappings();
        
        // Add services to the container.
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        services.AddOpenApi();
        return services;
    }
}