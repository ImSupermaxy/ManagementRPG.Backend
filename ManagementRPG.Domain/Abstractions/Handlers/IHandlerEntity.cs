using ManagementRPG.Domain.Abstractions.Commands.Inserts;
using ManagementRPG.Domain.Abstractions.Commands.Updates;
using ManagementRPG.Domain.Abstractions.Responses;
using ManagementRPG.Domain.Shared.Commands;

namespace ManagementRPG.Domain.Abstractions.Handlers
{
    public interface IHandlerEntity<TId, TInsert, TUpdate> : IHandler
        where TInsert : ICommandInsert
        where TUpdate : ICommandUpdate<TId>
    {
        
        public Task<Result<TId>> HandleInsert(TInsert command);
        public Task<Result> HandleUpdate(TUpdate command);
    }

    public interface IHandlerEntity<TId, TUId, TInsert, TUpdate>
           : IHandlerEntity<TId, TInsert, TUpdate>
       where TInsert : ICommandInsert<TUId>
       where TUpdate : ICommandUpdate<TId, TUId>
    {
    }

    public interface IHandlerQuery<TResponse, TId> : IHandler
        where TResponse : IResponse<TId>
    {
        public Task<Result<IEnumerable<TResponse>>> HandleGetAll();
        public Task<Result<TResponse>> HandleGet(TId id);
    }

    public interface IHandlerQuery<TResponse, TId, TUId> : IHandlerQuery<TResponse, TId>
        where TResponse : IResponse<TId, TUId>
    {

    }
}
