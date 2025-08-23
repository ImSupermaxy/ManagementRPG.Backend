using ManagementRPG.Domain.Abstractions.Commands.Results;

namespace ManagementRPG.Domain.Shared.Commands
{
    public class CommandResult : ICommandResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }

        public CommandResult(bool success, string message)
        {
            Success = success;
            Message = message;
            Data = null!;
        }

        public CommandResult(bool success, string message, dynamic data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }

    public class CommandResult<T> : ICommandResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public CommandResult(bool success, string message)
        {
            Success = success;
            Message = message;
            Data = default!;
        }

        public CommandResult(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}
