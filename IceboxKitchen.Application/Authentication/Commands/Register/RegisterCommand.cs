using ErrorOr;
using IceboxKitchen.Application.Authentication.Common;
using MediatR;

namespace IceboxKitchen.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;