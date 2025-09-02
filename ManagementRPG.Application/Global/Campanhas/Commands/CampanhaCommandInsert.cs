using ManagementRPG.Domain.Abstractions.Commands.Inserts;
using ManagementRPG.Domain.Global.Campanhas;

namespace ManagementRPG.Application.Global.Campanhas.Commands
{
    public sealed class CampanhaCommandInsert : CampanhaGeneral, ICommandInsert<int>
    {
        public int UserId { get; set; }
    }
}
