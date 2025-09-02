using ManagementRPG.Application.Abstractions.Clock;

namespace ManagementRPG.Infrastructure.Clock
{
    internal sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}

