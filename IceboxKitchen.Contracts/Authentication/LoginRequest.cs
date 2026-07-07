namespace IceboxKitchen.Contracts.Authentication;

public record LoginRequest(
    string Email,
    string Password
);