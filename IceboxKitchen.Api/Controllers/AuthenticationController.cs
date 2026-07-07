using Microsoft.AspNetCore.Mvc;
using IceboxKitchen.Contracts.Authentication;

namespace IceboxKitchen.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    [Route("register")]
    public IActionResult Register(RegisterRequest request)
    {
        return Ok(request);
    }

    [Route("login")]
    public IActionResult Login(LoginRequest request)
    {
        return Ok(request);
    
    }
    
}