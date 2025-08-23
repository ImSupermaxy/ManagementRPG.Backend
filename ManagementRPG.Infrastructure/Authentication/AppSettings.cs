namespace ManagementRPG.Infrastructure.Authentication
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int ExpirationHours { get; set; }
        public string Sender { get; set; }
        public string ValidAt { get; set; }
    }
}
