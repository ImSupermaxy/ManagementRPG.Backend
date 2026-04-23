using Dapper;
using ManagementRPG.Domain.Abstractions.Context;
using ManagementRPG.Domain.Abstractions.Entities;
using ManagementRPG.Domain.Abstractions.Responses;
using ManagementRPG.Domain.Shared.Commands;
using System.Data;
using System.Linq;

namespace ManagementRPG.Domain.Abstractions.Repositories
{
    public abstract class Repository<T, TId, TResponse> : IRepository<T, TId, TResponse>
        where T : Entity<TId>
        where TResponse : IResponse<TId>
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

        public async Task<IEnumerable<TResponse>> GetAll()
        {
            return await Uow.Context.Connection.QueryAsync<TResponse>(
                $"SELECT * FROM {GetProcEntityName()}getAll()",
                commandType: CommandType.Text
            );
        }

        public async Task<TResponse> GetById(TId id)
        {
            return await Uow.Context.Connection.QueryFirstOrDefaultAsync<TResponse>(
                $"SELECT * FROM {GetProcEntityName()}GetById(@p_id)",
                new { p_id = id},
                commandType: CommandType.Text
            ) ?? default!;
        }

        public async Task<TResponse> GetByPropertys(List<DataParam> props, string customName = default!)
        {
            return await GetByPropertys<TResponse>(props, customName);
        }

        public async Task<IEnumerable<TResponse>> GetAllByPropertys(List<DataParam> props, string customName = default!)
        {
            return await GetAllByPropertys<TResponse>(props, customName);
        }

        public async Task<TResult> GetByPropertys<TResult>(List<DataParam> props, string customName = default!)
            where TResult : IResponse<TId>
        {
            var paramsString = "";
            var param = new DynamicParameters();

            foreach (var prop in props)
            {
                param.Add("p_" + prop.ParamName.ToLower(), prop.ParamValue);
            }

            paramsString = string.Join(',', props.Select(p => "@p_" + p.ParamName.ToLower()));

            return await Uow.Context.Connection.QueryFirstOrDefaultAsync<TResult>(
                $"SELECT * FROM {GetProcEntityName(customName)}get({paramsString})",
                param,
                commandType: CommandType.Text
            ) ?? default!;
        }

        public async Task<IEnumerable<TResult>> GetAllByPropertys<TResult>(List<DataParam> props, string customName = default!)
            where TResult : IResponse<TId>
        {
            var paramsString = "";
            var param = new DynamicParameters();

            foreach (var prop in props)
            {
                param.Add("p_" + prop.ParamName.ToLower(), prop.ParamValue);
            }

            paramsString = string.Join(',', props.Select(p => "@p_" + p.ParamName.ToLower()));

            return await Uow.Context.Connection.QueryAsync<TResult>(
                $"SELECT * FROM {GetProcEntityName(customName)}get({paramsString})",
                param,
                commandType: CommandType.Text
            ) ?? default!;
        }

        public async Task<TId> Insert(T entity)
        {
            var props = typeof(T).GetProperties()
                .Select(p => "@p_" + p.Name.ToLower())
                .Except(["@p_id", "@p_isvalid", "@p_errors"]);

            return await Uow.Context.Connection.ExecuteScalarAsync<TId>(
                $"SELECT {GetProcEntityName()}insert({string.Join(',', props)})", 
                GetInsertObject(entity), 
                transaction: Uow.Transaction
            ) ?? default!;
        }

        public async Task<bool> Update(T entity)
        {
            var props = typeof(T).GetProperties()
                .Select(p => "@p_" + p.Name.ToLower())
                .Except(["@p_isvalid", "@p_errors"]);

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

    public abstract class Repository<T, TId, TUId, TResponse> : Repository<T, TId, TResponse>,
            IRepository<T, TId, TUId, TResponse>
        where T : Entity<TId, TUId>
        where TResponse : IResponse<TId, TUId>
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
