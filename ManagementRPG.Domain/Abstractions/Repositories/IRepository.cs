using ManagementRPG.Domain.Abstractions.Entities;
using ManagementRPG.Domain.Abstractions.Queries.Results;

namespace ManagementRPG.Domain.Abstractions.Repositories
{
    public interface IRepository<T, TId, TCommandQuery> : IBaseRepository
        where T : Entity<TId>
        where TCommandQuery : IQueryResult<TId>
    {
        Task<IEnumerable<TCommandQuery>> Get();
        Task<TCommandQuery> GetById(TId id);

        //Task<TCommandQuery> GetByProperty<TProp>(TProp prop, string name);
        //Task<IEnumerable<TCommandQuery>> GetAllByProperty<TProp>(TProp prop, string name);

        Task<TId> Insert(T entity);
        Task<bool> Update(T entity);
    }

    public interface IRepository<T, TId, TUId, TCommandQuery> : IRepository<T, TId, TCommandQuery>
        where T : Entity<TId, TUId>
        where TCommandQuery : IQueryResult<TId, TUId>
    {
        Task<bool> Delete(T entity);
    }
}
