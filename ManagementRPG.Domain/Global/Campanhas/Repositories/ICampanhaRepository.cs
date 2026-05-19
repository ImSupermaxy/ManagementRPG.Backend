using ManagementRPG.Domain.Abstractions.Repositories;
using ManagementRPG.Domain.Global.Campanhas.Entities;
using ManagementRPG.Domain.Global.Campanhas.Responses;

namespace ManagementRPG.Domain.Global.Campanhas.Repositories
{
    public interface ICampanhaRepository : IRepository<Campanha, int, int, CampanhaResponse>
    {
        Task<IEnumerable<CampanhaResponse>> GetByJogadorId(int jogadorId);
        Task<IEnumerable<CampanhaResponse>> GetByMestreId(int mestreId);
    }
}
