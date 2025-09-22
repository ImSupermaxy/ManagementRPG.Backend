using ManagementRPG.Domain.Abstractions.Commands.Inserts;
using ManagementRPG.Domain.Abstractions.Commands.Updates;
using ManagementRPG.Domain.Abstractions.Entities;
using ManagementRPG.Domain.Abstractions.Queries.Results;
using ManagementRPG.Domain.Shared.Commands;

namespace ManagementRPG.Domain.Abstractions.Handlers
{
    public interface IHandlerEntity<TId, TCommandInsert, TCommandUpdate> : IHandler
        where TCommandInsert : ICommandInsert
        where TCommandUpdate : ICommandUpdate<TId>
    {
        
        public Task<Result> HandleInsert(TCommandInsert command);
        public Task<Result> HandleUpdate(TCommandUpdate command);
    }

    public interface IHandlerEntity<TId, TUId, TCommandInsert, TCommandUpdate>
           : IHandlerEntity<TId, TCommandInsert, TCommandUpdate>
       where TCommandInsert : ICommandInsert<TUId>
       where TCommandUpdate : ICommandUpdate<TId, TUId>
    {
    }

    public interface IHandlerQuery<TCommandQuery, TId> : IHandler
        where TCommandQuery : IQueryResult<TId>
    {
        public Task<Result<IEnumerable<TCommandQuery>>> HandleGetAll();
        public Task<Result<TCommandQuery>> HandleGet(TId id);
    }

    public interface IHandlerQuery<TCommandQuery, TId, TUId> : IHandlerQuery<TCommandQuery, TId>
        where TCommandQuery : IQueryResult<TId, TUId>
    {

    }
}
