using ManagementRPG.Domain.Entities.Campanhas;
using ManagementRPG.Domain.Queries.Campanhas;
using ManagementRPG.Domain.Abstractions.Repositories;

namespace ManagementRPG.Domain.Repositories.Campanhas
{
    public interface ICampanhaRepository : IRepository<Campanha, int, int, CampanhaQueryResult>
    {
    }
}
