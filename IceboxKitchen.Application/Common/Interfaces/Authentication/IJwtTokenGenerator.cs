using IceboxKitchen.Domain.Entities;

namespace IceboxKitchen.Application.Common.Interfaces.Authentication;
public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}