using ManagementRPG.Domain.Abstractions.Queries.Results;
using ManagementRPG.Domain.Shared.Enums;

namespace ManagementRPG.Domain.Queries.Campanhas
{
    public class CampanhaQueryResult : IQueryResult<int, int>
    {
        public int MestreId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Sinopse { get; set; }

        public EStatus Status { get; set; }
        public int UserInsId { get; set; }
        public DateTime UserInsData { get; set; }
        public int UserModId { get; set; }
        public DateTime UserModData { get; set; }

        public int Id { get; set; }
    }
}
