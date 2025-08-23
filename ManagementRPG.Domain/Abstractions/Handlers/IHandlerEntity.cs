using ManagementRPG.Domain.Abstractions.Commands.Inserts;
using ManagementRPG.Domain.Abstractions.Commands.Updates;
using ManagementRPG.Domain.Abstractions.Entities;
using ManagementRPG.Domain.Abstractions.Queries.Results;
using ManagementRPG.Domain.Shared.Commands;

namespace ManagementRPG.Domain.Abstractions.Handlers
{
    public interface IHandlerEntity<T, TId, TCommandInsert, TCommandUpdate, TCommandQuery> : IHandler
        where T : Entity<TId>
        where TCommandInsert : ICommandInsert
        where TCommandUpdate : ICommandUpdate<TId>
        where TCommandQuery : IQueryResult<TId>
    {
        public Task<CommandResult<IEnumerable<TCommandQuery>>> HandleGetAll();
        public Task<CommandResult<TCommandQuery>> HandleGet(TId id);
        public Task<CommandResult> HandleInsert(TCommandInsert command);
        public Task<CommandResult> HandleUpdate(TCommandUpdate command);
    }

    public interface IHandlerEntity<T, TId, TUId, TCommandInsert, TCommandUpdate, TCommandQuery>
           : IHandlerEntity<T, TId, TCommandInsert, TCommandUpdate, TCommandQuery>
       where T : Entity<TId, TUId>
       where TCommandInsert : ICommandInsert<TUId>
       where TCommandUpdate : ICommandUpdate<TId, TUId>
        where TCommandQuery : IQueryResult<TId>
    {
        public Task<CommandResult> HandleRemove(TId id);
    }
}
