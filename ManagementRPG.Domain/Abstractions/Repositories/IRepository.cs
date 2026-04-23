using ManagementRPG.Domain.Abstractions.Entities;
using ManagementRPG.Domain.Abstractions.Responses;
using ManagementRPG.Domain.Shared.Commands;

namespace ManagementRPG.Domain.Abstractions.Repositories
{
    public interface IRepository<T, TId, TResponse> : IBaseRepository
        where T : Entity<TId>
        where TResponse : IResponse<TId>
    {
        Task<IEnumerable<TResponse>> GetAll();
        Task<TResponse> GetById(TId id);
        Task<TResult> GetByPropertys<TResult>(List<DataParam> props, string customName = default!)
            where TResult : IResponse<TId>;
        Task<IEnumerable<TResult>> GetAllByPropertys<TResult>(List<DataParam> props, string customName = default!)
            where TResult : IResponse<TId>;
        Task<TId> Insert(T entity);
        Task<bool> Update(T entity);
    }

    public interface IRepository<T, TId, TUId, TResponse> : IRepository<T, TId, TResponse>
        where T : Entity<TId, TUId>
        where TResponse : IResponse<TId, TUId>
    {
        Task<bool> Delete(T entity);
    }
}
