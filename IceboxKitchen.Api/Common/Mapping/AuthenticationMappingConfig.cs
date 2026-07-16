using IceboxKitchen.Application.Authentication.Commands.Register;
using IceboxKitchen.Application.Authentication.Common;
using IceboxKitchen.Application.Authentication.Queries.Login;
using IceboxKitchen.Contracts.Authentication;
using Mapster;

namespace IceboxKitchen.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest, src => src.User);
    }
}