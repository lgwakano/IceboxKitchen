using IceboxKitchen.Domain.Entities;

namespace IceboxKitchen.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        User? GetUserByEmail(string email);
        void AddUser(User user);
    }
}