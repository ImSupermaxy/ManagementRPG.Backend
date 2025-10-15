using ManagementRPG.Domain.Shared.ApiConfig.Authentication;

namespace ManagementRPG.Infrastructure.Authentication
{
    public class AppAuthSettings : IAppAuthSettings
    {
        public string Secret { get; init; }
        public int ExpirationHours { get; init; }
        public string Sender { get; init; }
        public string ValidAt { get; init; }
    }
}
