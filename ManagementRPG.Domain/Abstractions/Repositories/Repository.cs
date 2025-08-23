using Dapper;
using ManagementRPG.Domain.Abstractions.Context;
using ManagementRPG.Domain.Abstractions.Entities;
using ManagementRPG.Domain.Abstractions.Queries.Results;
using System.Data;

namespace ManagementRPG.Domain.Abstractions.Repositories
{
    public class Repository<T, TId, TCommandQuery> : IRepository<T, TId, TCommandQuery>
        where T : Entity<TId>
        where TCommandQuery : IQueryResult<TId>
    {
        protected readonly IUnitOfWork Uow;
        private string _numberTable = "";
        private string _customName = "";

        public Repository(IUnitOfWork uow, string numberTable, string customName = "")
        {
            Uow = uow;

            //throw new CustomException();
            if (string.IsNullOrEmpty(numberTable))
                throw new Exception("Número da tabela não é válido");

            if (numberTable.Any(c => !char.IsNumber(c)))
                throw new Exception("Número da tabela não é válido");

            _numberTable = numberTable;
            _customName = customName ?? "";
        }

        public async Task<IEnumerable<TCommandQuery>> Get()
        {
            var param = new DynamicParameters();

            //param.Add("chave", base.keyCryptDecrypt);

            return await Uow.Context.Connection
                                .QueryAsync<TCommandQuery>($"{GetProcEntityName()}Get",
                                    param,
                                    commandType: CommandType.StoredProcedure);
        }

        public async Task<TCommandQuery> GetById(TId id)
        {
            var param = new DynamicParameters();

            //param.Add("chave", base.keyCryptDecrypt);
            param.Add("id", id);

            return (await Uow.Context.Connection
                                .QueryFirstOrDefaultAsync<TCommandQuery>($"{GetProcEntityName()}Get",
                                    param,
                                    commandType: CommandType.StoredProcedure))!;
        }

        public Task<IEnumerable<TCommandQuery>> GetByProperty<TProp>(TProp id)
        {
            //Elaborate better how implemente this...
            throw new NotImplementedException();
        }

        public async Task<TId> Insert(T entity)
        {
            return await Uow.Context.Connection
                                .QueryFirstAsync<TId>($"{GetProcEntityName()}Insert",
                                    GetInsertObject(entity),
                                    transaction: Uow.Transaction,
                                    commandType: CommandType.StoredProcedure) ?? default!;
        }

        public async Task<bool> Update(T entity)
        {
            var rows = await Uow.Context.Connection
                                .QueryFirstAsync<int>($"{GetProcEntityName()}Update",
                                    GetInsertObject(entity),
                                    transaction: Uow.Transaction,
                                    commandType: CommandType.StoredProcedure);
            return rows == 1;
        }

        protected string GetProcEntityName()
        {
            return $"sp{_customName}{_numberTable}";
        }

        protected virtual object GetInsertObject(T entity)
        {
            return new { };
        }

        protected virtual object GetUpdateObject(T entity)
        {
            return new { };
        }
    }

    public class Repository<T, TId, TUId, TCommandQuery> : Repository<T, TId, TCommandQuery>,
            IRepository<T, TId, TUId, TCommandQuery>
        where T : Entity<TId, TUId>
        where TCommandQuery : IQueryResult<TId, TUId>
    {
        public Repository(IUnitOfWork uow, string numberTable, string customName = "") 
            : base(uow, numberTable, customName)
        {
        }

        public async Task<bool> Delete(T entity)
        {
            var rows = await Uow.Context.Connection
                    .QueryFirstAsync<int>($"{GetProcEntityName()}Update",
                        GetInsertObject(entity),
                        transaction: Uow.Transaction,
                        commandType: CommandType.StoredProcedure);

            return rows == 1;
        }
    }
}
