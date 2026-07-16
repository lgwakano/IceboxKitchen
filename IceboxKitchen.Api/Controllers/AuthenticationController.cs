using ErrorOr;
using IceboxKitchen.Application.Authentication.Commands.Register;
using IceboxKitchen.Application.Authentication.Queries.Login;
using IceboxKitchen.Application.Authentication.Common;
using IceboxKitchen.Contracts.Authentication;
using IceboxKitchen.Domain.Common.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IceboxKitchen.Api.Controllers;

[Route("auth")]
public class AuthenticationController(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator;
    [Route("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors));
    }

    [Route("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = new LoginQuery(
            request.Email,
            request.Password);

        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(query);

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