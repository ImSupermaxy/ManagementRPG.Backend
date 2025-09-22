using ManagementRPG.Domain.Shared.Commands;
using MediatR;

namespace ManagementRPG.Domain.Abstractions.Commands.Handlers
{
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
        where TCommand : ICommandResponse
    {
    }

    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
        where TCommand : ICommandResponse<TResponse>
    {
    }
}
