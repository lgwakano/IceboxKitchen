using ErrorOr;
using IceboxKitchen.Application.Common.Interfaces.Authentication;
using IceboxKitchen.Application.Common.Interfaces.Persistence;
using IceboxKitchen.Application.Authentication.Common;
using IceboxKitchen.Domain.Common.Errors;
using IceboxKitchen.Domain.Entities;
using MediatR;

namespace IceboxKitchen.Application.Authentication.Queries.Login;

public class LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository) :
    IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        //1. Check if user exists
        if (userRepository.GetUserByEmail(query.Email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        //2. Check if password is correct
        if (user.Password != query.Password)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        //3. Create JWT token
        string token = jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }
}