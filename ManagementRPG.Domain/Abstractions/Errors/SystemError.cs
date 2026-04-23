namespace ManagementRPG.Domain.Abstractions.Errors
{
    public static class SystemError
    {
        public static readonly Error Offline = new(
            "Notification.SystemOffline",
            $"The System is Offline");

        public static readonly Error Maintenance = new(
            "Notification.SystemMaintenance",
            $"The System is in Maintenance");

        public static readonly Error Deprecated = new(
            "Notification.SystemDeprecated",
            $"System is Deprecated, please update version");

        public static readonly Error LostDatabaseConnection = new(
            "Notification.SystemLostDatabaseConnenction",
            $":( you maybe lost connection with us, refresh or verify your connection with internt");

        public static readonly Error GenericError = new(
            "Notification.SystemGenericError",
            $"An or more errors occurred");
    }
}
