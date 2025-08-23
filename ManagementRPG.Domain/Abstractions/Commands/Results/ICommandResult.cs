namespace ManagementRPG.Domain.Abstractions.Commands.Results
{
    public interface ICommandResult : ICommand
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public interface ICommandResult<T> : ICommandResult
    {
        public T Data { get; set; }
    }
}
