using ManagementRPG.Domain.Shared.Commands;
using MediatR;

namespace ManagementRPG.Domain.Abstractions.Commands
{
    public interface ICommandResponse : ICommand, IRequest<CommandResult>
    {
    }

    public interface ICommandResponse<T> : ICommand, IRequest<CommandResult<T>>
    {
    }
}
