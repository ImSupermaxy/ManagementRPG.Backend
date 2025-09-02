using ManagementRPG.Domain.Abstractions.Queries.Results;
using ManagementRPG.Domain.Shared.Enums;

namespace ManagementRPG.Domain.Global.Campanhas.Queries
{
    public sealed class CampanhaQueryResult : CampanhaGeneral, IQueryResult<int, int>
    {
        public EStatus Status { get; set; }
        public int UserInsId { get; set; }
        public DateTime UserInsData { get; set; }
        public int UserModId { get; set; }
        public DateTime UserModData { get; set; }

        public int Id { get; set; }
    }
}
