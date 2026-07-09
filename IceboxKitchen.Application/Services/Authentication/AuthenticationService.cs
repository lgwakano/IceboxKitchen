using IceboxKitchen.Application.Common.Interfaces.Authentication;

namespace IceboxKitchen.Application.Services.Authentication;

public class AuthenticationService(IJwtTokenGenerator jwtTokenGenerator) : IAuthenticationService
{
    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        //Check if user already exists - same email

        //Create user - generate unique id

        //Create JWT token
        Guid userId = Guid.NewGuid();
        
        string token = jwtTokenGenerator.GenerateToken(userId, firstName, lastName);

        return new AuthenticationResult(
            userId, 
            firstName, 
            lastName, 
            email, 
            token);
    }

    public AuthenticationResult Login(string email, string password)
    {
        return new AuthenticationResult(Guid.NewGuid(), "John", "Doe", email, "token");
    }
}