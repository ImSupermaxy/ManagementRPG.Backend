namespace ManagementRPG.Domain.Abstractions.Messages.Errors
{
    public sealed record Error(string Code, string Name, List<string>? Messages = null) : IMessage
    {
        public static readonly Error None = new(string.Empty, string.Empty);

        public static readonly Error NullValue = new("Error.NullValue", "Null value was provided");

        public static Error FromValidationErrors(string code, string name, List<string> messages)
        {
            return new Error(code, name, messages);
        }

        public static Error FromValidationError(string code, string name, string message)
        {
            return new Error(code, name, [message]);
        }

        public override string ToString()
        {
            if (Messages != null && Messages.Any())
                return $"{Name} ({Code}): {string.Join(", ", Messages)}";

            return $"{Name} ({Code})";
        }
    }
}
