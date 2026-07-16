using IceboxKitchen.Domain.Entities;

namespace IceboxKitchen.Application.Authentication.Common;
public record AuthenticationResult(
    User user,
    string Token
);
