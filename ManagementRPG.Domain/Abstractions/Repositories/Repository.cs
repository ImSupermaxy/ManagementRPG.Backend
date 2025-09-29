using Dapper;
using ManagementRPG.Domain.Abstractions.Context;
using ManagementRPG.Domain.Abstractions.Entities;
using ManagementRPG.Domain.Abstractions.Queries.Results;
using System.Data;
using System.Linq;

namespace ManagementRPG.Domain.Abstractions.Repositories
{
    public abstract class Repository<T, TId, TCommandQuery> : IRepository<T, TId, TCommandQuery>
        where T : Entity<TId>
        where TCommandQuery : IQueryResult<TId>
    {
        protected readonly IUnitOfWork Uow;
        private string _numberTable = "";

        public Repository(IUnitOfWork uow, string numberTable)
        {
            Uow = uow;

            if (string.IsNullOrEmpty(numberTable))
                throw new Exception("Número da tabela não é válido");

            if (numberTable.Any(c => !char.IsNumber(c)))
                throw new Exception("Número da tabela não é válido");

            _numberTable = numberTable;
        }

        public async Task<IEnumerable<TCommandQuery>> GetAll()
        {
            return await Uow.Context.Connection.QueryAsync<TCommandQuery>(
                $"SELECT * FROM {GetProcEntityName()}getAll()",
                commandType: CommandType.Text
            );
        }

        public async Task<TCommandQuery> GetById(TId id)
        {
            return await Uow.Context.Connection.QueryFirstOrDefaultAsync<TCommandQuery>(
                $"SELECT * FROM {GetProcEntityName()}GetById(@p_id)",
                new { p_id = id},
                commandType: CommandType.Text
            ) ?? default!;
        }

        public async Task<TCommandQuery> GetByPropertys(List<Tuple<object, string>> props, string customName = default!)
        {
            return await GetByPropertys<TCommandQuery>(props, customName);
        }

        public async Task<IEnumerable<TCommandQuery>> GetAllByPropertys(List<Tuple<object, string>> props, string customName = default!)
        {
            return await GetAllByPropertys<TCommandQuery>(props, customName);
        }

        public async Task<TResult> GetByPropertys<TResult>(List<Tuple<object, string>> props, string customName = default!)
            where TResult : IQueryResult<TId>
        {
            var paramsString = "";
            var param = new DynamicParameters();

            foreach (var prop in props)
            {
                param.Add("p_" + prop.Item2.ToLower(), prop.Item1);
            }

            paramsString = string.Join(',', props.Select(p => "@p_" + p.Item2.ToLower()));

            return await Uow.Context.Connection.QueryFirstOrDefaultAsync<TResult>(
                $"SELECT * FROM {GetProcEntityName(customName)}get({paramsString})",
                param,
                commandType: CommandType.Text
            ) ?? default!;
        }

        public async Task<IEnumerable<TResult>> GetAllByPropertys<TResult>(List<Tuple<object, string>> props, string customName = default!)
            where TResult : IQueryResult<TId>
        {
            var paramsString = "";
            var param = new DynamicParameters();

            foreach (var prop in props)
            {
                param.Add("p_" + prop.Item2.ToLower(), prop.Item1);
            }

            paramsString = string.Join(',', props.Select(p => "@p_" + p.Item2.ToLower()));

            return await Uow.Context.Connection.QueryAsync<TResult>(
                $"SELECT * FROM {GetProcEntityName(customName)}get({paramsString})",
                param,
                commandType: CommandType.Text
            ) ?? default!;
        }

        public async Task<TId> Insert(T entity)
        {
            var props = typeof(T).GetProperties().Select(p => "@p_" + p.Name.ToLower()).Except(["@p_id", "@p_isvalid", "@p_errors"]);

            return await Uow.Context.Connection.ExecuteScalarAsync<TId>(
                $"SELECT {GetProcEntityName()}insert({string.Join(',', props)})", 
                GetInsertObject(entity), 
                transaction: Uow.Transaction
            ) ?? default!;
        }

        public async Task<bool> Update(T entity)
        {
            var props = typeof(T).GetProperties().Select(p => "@p_" + p.Name.ToLower()).Except(["@p_isvalid", "@p_errors"]);

            var rows = await Uow.Context.Connection.ExecuteScalarAsync<int>(
                $"SELECT {GetProcEntityName()}update({string.Join(',', props)})", 
                GetUpdateObject(entity), 
                transaction: Uow.Transaction
            );

            return rows == 1;
        }

        protected string GetProcEntityName(string customName = default!)
        {
            return $"sp{customName}{_numberTable}";
        }

        protected abstract object GetInsertObject(T entity);

        protected abstract object GetUpdateObject(T entity);
    }

    public abstract class Repository<T, TId, TUId, TCommandQuery> : Repository<T, TId, TCommandQuery>,
            IRepository<T, TId, TUId, TCommandQuery>
        where T : Entity<TId, TUId>
        where TCommandQuery : IQueryResult<TId, TUId>
    {
        public Repository(IUnitOfWork uow, string numberTable) 
            : base(uow, numberTable)
        {
        }

        public async Task<bool> Delete(T entity)
        {
            var rows = await Uow.Context.Connection.ExecuteScalarAsync<int>(
                $"SELECT {GetProcEntityName()}delete(@p_id)",
                new { p_id = entity.Id },
                transaction: Uow.Transaction
            );

            return rows == 1;
        }
    }
}
