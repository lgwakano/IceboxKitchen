using IceboxKitchen.Application.Common.Interfaces.Authentication;
using IceboxKitchen.Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace IceboxKitchen.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        return services;
    }
}