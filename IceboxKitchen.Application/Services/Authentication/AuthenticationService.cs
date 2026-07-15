using ErrorOr;
using IceboxKitchen.Application.Common.Interfaces.Authentication;
using IceboxKitchen.Application.Common.Interfaces.Persistence;
using IceboxKitchen.Domain.Common.Errors;
using IceboxKitchen.Domain.Entities;

namespace IceboxKitchen.Application.Services.Authentication;

public class AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository) : IAuthenticationService
{
    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        //1. Check if user does not exist
        if (userRepository.GetUserByEmail(email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        //2. Create user - generate unique id
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };
        userRepository.AddUser(user);

        //3. Create JWT token
        string token = jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        //1. Check if user exists
        if (userRepository.GetUserByEmail(email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        //2. Check if password is correct
        if (user.Password != password)
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