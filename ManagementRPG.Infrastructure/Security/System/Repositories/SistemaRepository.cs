using Dapper;
using ManagementRPG.Domain.Abstractions.Context;
using ManagementRPG.Domain.Abstractions.Entities;
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
        public SistemaRepository(IUnitOfWork uow) 
            : base(uow, "001")
        {
        }

        public async Task<SistemaQueryResult> GetActive()
        {
            var param = GetDefaultParams(status: EStatusSistema.Online);
            return await GetByPropertys(param);
        }

        public async Task<EStatusSistema?> GetStatusSistema()
        {
            return (await GetLastSistema()).Status;
        }

        public async Task<SistemaQueryResult> GetLastSistema()
        {
            return await Uow.Context.Connection.QueryFirstOrDefaultAsync<SistemaQueryResult>(
                $"SELECT * FROM {GetProcEntityName()}getlast()"
            ) ?? default!;
        }

        protected override object GetInsertObject(Sistema entity)
        {
            return new
            {
                p_nome = entity.Nome,
                p_versao = entity.Versao,
                p_status = entity.Status,
                p_userinsid = entity.UserInsId,
                p_usermodid = entity.UserModId,
                p_userinsdata = entity.UserInsData,
                p_usermoddata = entity.UserModData,
            };
        }

        protected override object GetUpdateObject(Sistema entity)
        {
            return new
            {
                p_id = entity.Id,
                p_nome = entity.Nome,
                p_versao = entity.Versao,
                p_status = entity.Status,
                p_userinsid = entity.UserInsId,
                p_usermodid = entity.UserModId,
                p_userinsdata = entity.UserInsData,
                p_usermoddata = entity.UserModData,
            };
        }

        private List<Tuple<object, string>> GetDefaultParams(int? id = null, EStatusSistema? status = null, string? versao = null)
        {
            var param = new List<Tuple<object, string>>
            {
                new Tuple<object, string>(id ?? default!, "id"),
                new Tuple<object, string>(status ?? default!, "status"),
                new Tuple<object, string>(versao ?? default !, "versao")
            };

            return param;
        }
    }
}
