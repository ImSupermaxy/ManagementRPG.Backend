using ManagementRPG.Domain.Abstractions.Responses;
using ManagementRPG.Domain.Security.System.Enums;

namespace ManagementRPG.Domain.Security.System.Responses
{
    public sealed class SistemaResponse : SistemaGeneral, IResponse<int, int>
    {
        public EStatusSistema Status { get; set; }
        public int UserInsId { get; set; }
        public DateTime UserInsData { get; set; }
        public int UserModId { get; set; }
        public DateTime UserModData { get; set; }

        public int Id { get; set; }
    }
}
