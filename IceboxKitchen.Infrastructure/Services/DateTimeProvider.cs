using IceboxKitchen.Application.Common.Interfaces.Providers;

namespace IceboxKitchen.Infrastructure.Services;
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}