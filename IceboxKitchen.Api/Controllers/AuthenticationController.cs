using Microsoft.AspNetCore.Mvc;
using IceboxKitchen.Contracts.Authentication;
using IceboxKitchen.Application.Services.Authentication;

namespace IceboxKitchen.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
{
    [Route("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authResult = authenticationService.Register(
            request.FirstName, 
            request.LastName, 
            request.Email, 
            request.Password);

        var response = new AuthenticationResponse(
            authResult.user.Id, 
            authResult.user.FirstName, 
            authResult.user.LastName, 
            authResult.user.Email, 
            authResult.Token);

        return Ok(response);
    }

    [Route("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = authenticationService.Login(
            request.Email, request.Password);

        var response = new AuthenticationResponse(
            authResult.user.Id, 
            authResult.user.FirstName, 
            authResult.user.LastName, 
            authResult.user.Email, 
            authResult.Token);

        return Ok(response);
    }
    
}