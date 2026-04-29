using ManagementRPG.Domain.Abstractions.Context;
using ManagementRPG.Domain.Abstractions.Repositories;
using ManagementRPG.Domain.Security.System.Entities;
using ManagementRPG.Domain.Security.System.Enums;
using ManagementRPG.Domain.Security.System.Repositories;
using ManagementRPG.Domain.Security.System.Responses;
using ManagementRPG.Domain.Shared.Commands;
using System.Data;

namespace ManagementRPG.Infrastructure.Security.System.Repositories
{
    public class SistemaRepository : Repository<Sistema, int, int, SistemaResponse>, ISistemaRepository
    {
        public SistemaRepository(IUnitOfWork uow) 
            : base(uow, "001")
        {
        }

        public async Task<bool> IsActive(int id)
        {
            var param = GetDefaultParams(id, status: EStatusSistema.Online);
            return (await GetByPropertys(param)) is not null;
        }

        protected override object GetInsertObject(Sistema entity)
        {
            return new
            {
                p_nome = entity.Nome,
                p_versao = entity.Versao,
                p_status = entity.Status,
                p_userinsid = entity.UserInsId,
                p_userinsdata = entity.UserInsData,
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
                p_usermodid = entity.UserModId,
                p_usermoddata = entity.UserModData,
            };
        }

        private List<DataParam> GetDefaultParams(int? id = null, EStatusSistema? status = null, string? versao = null)
        {
            var param = new List<DataParam>
            {
                new DataParam("id", id ?? default!, DbType.Int32),
                new DataParam("status", status ?? default!, DbType.Int16),
                new DataParam("versao", versao ?? default!, DbType.String)
            };

            return param;
        }
    }
}
