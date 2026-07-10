using IceboxKitchen.Application.Common.Interfaces.Authentication;
using IceboxKitchen.Application.Common.Interfaces.Persistence;
using IceboxKitchen.Domain.Entities;

namespace IceboxKitchen.Application.Services.Authentication;

public class AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository) : IAuthenticationService
{
    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        //1. Check if user does not exist
        if (userRepository.GetUserByEmail(email) is not null)
        {
            throw new InvalidOperationException("User with this email already exists.");
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

    public AuthenticationResult Login(string email, string password)
    {
        //1. Check if user exists
        if (userRepository.GetUserByEmail(email) is not User user)
        {
            //Not safe to return user information need to change later to not give away if user exists or not
            throw new InvalidOperationException("User with this email does not exist.");
        }

        //2. Check if password is correct
        if (user.Password != password)
        {
            throw new InvalidOperationException("Invalid password.");
        }

        //3. Create JWT token
        string token = jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }
}