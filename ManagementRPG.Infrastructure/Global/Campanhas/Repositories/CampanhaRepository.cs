using Dapper;
using ManagementRPG.Domain.Abstractions.Context;
using ManagementRPG.Domain.Abstractions.Repositories;
using ManagementRPG.Domain.Global.Campanhas.Entities;
using ManagementRPG.Domain.Global.Campanhas.Repositories;
using ManagementRPG.Domain.Global.Campanhas.Responses;
using System.Data;
using System.Text;

namespace ManagementRPG.Infrastructure.Global.Campanhas.Repositories
{
    public class CampanhaRepository : Repository<Campanha, int, int, CampanhaResponse>, ICampanhaRepository
    {
        public CampanhaRepository(IUnitOfWork uow)
            : base(uow, "100")
        {
        }

        public async Task<IEnumerable<CampanhaResponse>> GetByJogadorId(int jogadorId)
        {
            var sql = new StringBuilder();

            //SELECT na tabela de join dos jogadores...
            sql.AppendLine($"""
                
                """);
            
            return await Uow.Context.Connection.QueryAsync<CampanhaResponse>(
                sql.ToString(),
                transaction: Uow.Transaction
            );
        }

        public async Task<IEnumerable<CampanhaResponse>> GetByMestreId(int mestreId)
        {
            var sql = new StringBuilder();

            sql.AppendLine($"""
                SELECT * FROM tbl_{_numberTable}_campanha t{_numberTable} WHERE t{_numberTable}.mestreid = {mestreId};
                """);
            
            return await Uow.Context.Connection.QueryAsync<CampanhaResponse>(
                sql.ToString(),
                commandType: CommandType.Text,
                transaction: Uow.Transaction
            );
        }

        protected override object GetInsertObject(Campanha entity)
        {
            throw new NotImplementedException();
        }

        protected override object GetUpdateObject(Campanha entity)
        {
            throw new NotImplementedException();
        }
    }
}
