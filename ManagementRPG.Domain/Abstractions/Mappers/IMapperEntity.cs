using ManagementRPG.Domain.Abstractions.Commands.Inserts;
using ManagementRPG.Domain.Abstractions.Commands.Updates;
using ManagementRPG.Domain.Abstractions.Entities;
using ManagementRPG.Domain.Abstractions.Queries.Results;

namespace ManagementRPG.Domain.Abstractions.Mappers
{
    public interface IMapperEntity<T, TId, TCommandInsert, TCommandUpdate, TCommandQuery> : IMapper
        where T : Entity<TId>
        where TCommandInsert : ICommandInsert
        where TCommandUpdate : ICommandUpdate<TId>
        where TCommandQuery : IQueryResult<TId>
    {
        T GetEntity(TCommandInsert command);
        T GetEntity(TCommandQuery oldEntity, TCommandUpdate command);
        T GetEntity(TCommandQuery command);
    }

    public interface IMapperEntity<T, TId, TUId, TCommandInsert, TCommandUpdate, TCommandQuery>
        : IMapperEntity<T, TId, TCommandInsert, TCommandUpdate, TCommandQuery>
        where T : Entity<TId, TUId>
        where TCommandInsert : ICommandInsert<TUId>
        where TCommandUpdate : ICommandUpdate<TId, TUId>
        where TCommandQuery : IQueryResult<TId, TUId>

    {

    }
}
