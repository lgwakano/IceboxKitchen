using System.Text;
using IceboxKitchen.Application.Common.Interfaces.Authentication;
using IceboxKitchen.Application.Common.Interfaces.Persistence;
using IceboxKitchen.Application.Common.Interfaces.Providers;
using IceboxKitchen.Infrastructure.Authentication;
using IceboxKitchen.Infrastructure.Persistence;
using IceboxKitchen.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;

namespace IceboxKitchen.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddAuthentication(configuration);
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    public static IServiceCollection AddAuthentication(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        //Register JwtSettings safely
        services.AddOptions<JwtSettings>()
            .Bind(configuration.GetSection(JwtSettings.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options=> options.TokenValidationParameters = new TokenValidationParameters()
            {
               //configure options
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration[JwtSettings.Issuer],
                ValidAudience = configuration[JwtSettings.Audience],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration[JwtSettings.Key])),
                ClockSkew = TimeSpan.Zero
            });
            
        return services;
    }
}