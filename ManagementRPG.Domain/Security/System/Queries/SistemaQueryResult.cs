using ManagementRPG.Domain.Abstractions.Queries.Results;
using ManagementRPG.Domain.Security.System.Enums;
using ManagementRPG.Domain.Shared.Enums;

namespace ManagementRPG.Domain.Security.System.Queries
{
    public sealed class SistemaQueryResult : SistemaGeneral, IQueryResult<int, int>
    {
        public EStatusSistema Status { get; set; }
        public int UserInsId { get; set; }
        public DateTime UserInsData { get; set; }
        public int UserModId { get; set; }
        public DateTime UserModData { get; set; }

        public int Id { get; set; }
    }
}
