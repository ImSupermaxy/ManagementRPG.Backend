namespace ManagementRPG.Domain.Shared.ApiConfig.Authentication
{
    public interface IAppAuthSettings
    {
        string Secret { get; init; }
        int ExpirationHours { get; init; }
        string Sender { get; init; }
        string ValidAt { get; init; }
    }
}
