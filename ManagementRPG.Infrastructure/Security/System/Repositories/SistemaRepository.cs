using Dapper;
using ManagementRPG.Domain.Abstractions.Context;
using ManagementRPG.Domain.Abstractions.Repositories;
using ManagementRPG.Domain.Security.System.Entities;
using ManagementRPG.Domain.Security.System.Enums;
using ManagementRPG.Domain.Security.System.Queries;
using ManagementRPG.Domain.Security.System.Repositories;
using System.Data;

namespace ManagementRPG.Infrastructure.Security.System.Repositories
{
    public class SistemaRepository : Repository<Sistema, int, int, SistemaQueryResult>, ISistemaRepository
    {
        public SistemaRepository(IUnitOfWork uow, string numberTable, string customName = "001") 
            : base(uow, numberTable, customName)
        {
        }

        public Task<SistemaQueryResult> GetActive()
        {
            throw new NotImplementedException();
        }



        public async Task<EStatusSistema?> GetStatusSistema()
        {
            //TODO: ATUALIZAAAAAAAAR
            var param = new DynamicParameters();

            //param.Add("chave", base.keyCryptDecrypt);
            param.Add("status", EStatusSistema.Online);

            return await Uow.Context.Connection
                        .QueryFirstOrDefaultAsync<EStatusSistema?>($"{GetProcEntityName()}Get",
                                    param,
                                    commandType: CommandType.StoredProcedure) ?? default!;
        }

        public Task<SistemaQueryResult> GetLastSistema()
        {
            throw new NotImplementedException();
        }
    }
}
