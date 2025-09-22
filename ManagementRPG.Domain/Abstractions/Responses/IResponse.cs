using ManagementRPG.Domain.Shared.Commands;
using MediatR;

namespace ManagementRPG.Domain.Abstractions.Responses
{
    public interface IResponse : IRequest<Result>
    {
    }
    public interface IResponse<T> : IRequest<Result<T>>
    {
    }
}
