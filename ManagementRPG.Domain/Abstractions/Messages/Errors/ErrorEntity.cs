namespace ManagementRPG.Domain.Abstractions.Messages.Errors
{
    public static class EntityError<T>
    {
        public static readonly Error NotFound = new(
            "Notification.EntityNotFound",
            $"Entity(s) {typeof(T).Name} Not Found.");

        public static readonly Error NotCreated = new(
            "Notification.EntityNotCreated",
            $"Entity(s) {typeof(T).Name} was Not Created.");

        public static readonly Error NotUpdated = new(
            "Notification.EntityNotUpdated",
            $"Entity(s) {typeof(T).Name} was Not Updated");

        public static readonly Error NotDeleted = new(
            "Notification.EntityNotDeleted",
            $"Entity(s) {typeof(T).Name} was Not Deleted");

        public static readonly Error Invalid = new(
            "Notification.EntityNotValid",
            $"Entity(s) {typeof(T).Name} is Not Valid");
    }
}
