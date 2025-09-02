using ManagementRPG.Domain.Abstractions.Repositories;
using ManagementRPG.Domain.Global.Campanhas.Entities;
using ManagementRPG.Domain.Global.Campanhas.Queries;

namespace ManagementRPG.Domain.Global.Campanhas.Repositories
{
    public interface ICampanhaRepository : IRepository<Campanha, int, int, CampanhaQueryResult>
    {
    }
}
