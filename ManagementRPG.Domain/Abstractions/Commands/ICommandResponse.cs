using ManagementRPG.Domain.Shared.Commands;
using MediatR;

namespace ManagementRPG.Domain.Abstractions.Commands
{
    public interface ICommandResponse : ICommand, IRequest<Result>
    {
    }

    public interface ICommandResponse<T> : ICommand, IRequest<Result<T>>
    {
    }
}
