using ManagementRPG.Domain.Shared.ApiConfig.Settings;

namespace ManagementRPG.Infrastructure.Authentication
{
    public class AppGeneralSettings : IAppGeneralSettings
    {
        public string Sender { get; init; }
    }

    public class AppAuthSettings : IAuthSettings
    {
        public required string Secret { get; init; }
        public required string Audience { get; init; }
        public int ExpirationHours { get; init; }
        public required string Sender { get; init; }
        public required string ValidAt { get; init; }
    }
}
