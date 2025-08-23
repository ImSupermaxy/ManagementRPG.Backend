using ManagementRPG.Domain.Shared.Commands;
using MediatR;

namespace ManagementRPG.Domain.Abstractions.Commands.Handlers
{
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, CommandResult>
        where TCommand : ICommandResponse
    {
    }

    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, CommandResult<TResponse>>
        where TCommand : ICommandResponse<TResponse>
    {
    }
}
