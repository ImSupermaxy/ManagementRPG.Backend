namespace ManagementRPG.Domain.Shared.ApiConfig.Settings
{
    public interface IAppGeneralSettings
    {
        string Sender { get; init; }
    }

    public interface IAuthSettings
    {
        string Secret { get; init; }
        string Audience { get; init; }
        int ExpirationHours { get; init; }
        string Sender { get; init; }
        string ValidAt { get; init; }
    }
}
