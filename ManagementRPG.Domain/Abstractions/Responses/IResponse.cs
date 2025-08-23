using ManagementRPG.Domain.Shared.Commands;
using MediatR;

namespace ManagementRPG.Domain.Abstractions.Responses
{
    public interface IResponse : IRequest<CommandResult>
    {
    }
    public interface IResponse<T> : IRequest<CommandResult<T>>
    {
    }
}
