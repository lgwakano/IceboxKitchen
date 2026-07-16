using ErrorOr;
using IceboxKitchen.Application.Authentication.Common;
using MediatR;

namespace IceboxKitchen.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;