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
        var result = authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);
        return Ok(result);
    }

    [Route("login")]
    public IActionResult Login(LoginRequest request)
    {
        var result = authenticationService.Login(request.Email, request.Password);
        return Ok(result);
    }
    
}