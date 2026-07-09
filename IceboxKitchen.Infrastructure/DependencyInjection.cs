using IceboxKitchen.Application.Common.Interfaces.Authentication;
using IceboxKitchen.Application.Common.Interfaces.Providers;
using IceboxKitchen.Infrastructure.Authentication;
using IceboxKitchen.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IceboxKitchen.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return services;
    }
}