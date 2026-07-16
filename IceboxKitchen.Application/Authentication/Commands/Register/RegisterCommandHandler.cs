using ErrorOr;
using IceboxKitchen.Application.Common.Interfaces.Authentication;
using IceboxKitchen.Application.Common.Interfaces.Persistence;
using IceboxKitchen.Application.Authentication.Common;
using IceboxKitchen.Domain.Common.Errors;
using IceboxKitchen.Domain.Entities;
using MediatR;

namespace IceboxKitchen.Application.Authentication.Commands.Register;

public class RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository) :
    IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        //1. Check if user does not exist
        if (userRepository.GetUserByEmail(command.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        //2. Create user - generate unique id
        var user = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = command.Password
        };
        userRepository.AddUser(user);

        //3. Create JWT token
        string token = jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }
}