using Microsoft.AspNetCore.Mvc;
using IceboxKitchen.Contracts.Authentication;
using IceboxKitchen.Application.Services.Authentication;
using ErrorOr;
using IceboxKitchen.Domain.Common.Errors;

namespace IceboxKitchen.Api.Controllers;

[Route("auth")]
public class AuthenticationController(IAuthenticationService authenticationService) : ApiController
{
    [Route("register")]
    public IActionResult Register(RegisterRequest request)
    {
        ErrorOr<AuthenticationResult> authResult = authenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors));
    }

    [Route("login")]
    public IActionResult Login(LoginRequest request)
    {
        ErrorOr<AuthenticationResult> authResult = authenticationService.Login(
            request.Email, request.Password);

        if(authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: authResult.FirstError.Description);
        }

        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors));
    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
                    authResult.user.Id,
                    authResult.user.FirstName,
                    authResult.user.LastName,
                    authResult.user.Email,
                    authResult.Token);
    }
    
}