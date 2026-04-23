using ManagementRPG.Domain.Abstractions.Context;
using ManagementRPG.Domain.Abstractions.Repositories;
using ManagementRPG.Domain.Global.Campanhas.Responses;
using ManagementRPG.Domain.Global.Campanhas.Repositories;
using ManagementRPG.Domain.Global.Campanhas.Entities;

namespace ManagementRPG.Infrastructure.Global.Campanhas.Repositories
{
    public class CampanhaRepository : Repository<Campanha, int, int, CampanhaResponse>, ICampanhaRepository
    {
        public CampanhaRepository(IUnitOfWork uow)
            : base(uow, "100")
        {
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
