using ErrorOr;
using IceboxKitchen.Application.Authentication.Commands.Register;
using IceboxKitchen.Application.Authentication.Queries.Login;
using IceboxKitchen.Application.Authentication.Common;
using IceboxKitchen.Contracts.Authentication;
using IceboxKitchen.Domain.Common.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MapsterMapper;
using Mapster;
using IceboxKitchen.Domain.Entities;

namespace IceboxKitchen.Api.Controllers;

[Route("auth")]
public class AuthenticationController(ISender mediator, IMapper mapper) : ApiController
{
    private readonly ISender _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    [Route("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);

        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

        return authResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            errors => Problem(errors));
    }

    [Route("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {

        var query = _mapper.Map<LoginQuery>(request);

        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(query);

        if(authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: authResult.FirstError.Description);
        }

        return authResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            errors => Problem(errors));
    }
    
}