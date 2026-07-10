using IceboxKitchen.Domain.Entities;

namespace IceboxKitchen.Application.Services.Authentication;
public record AuthenticationResult(
    User user,
    string Token
);
