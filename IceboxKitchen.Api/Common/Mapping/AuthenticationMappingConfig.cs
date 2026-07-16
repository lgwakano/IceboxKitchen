using IceboxKitchen.Application.Authentication.Common;
using IceboxKitchen.Contracts.Authentication;
using Mapster;

namespace IceboxKitchen.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest, src => src.User);
    }
}