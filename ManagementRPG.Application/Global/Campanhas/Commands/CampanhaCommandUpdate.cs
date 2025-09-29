using ManagementRPG.Domain.Abstractions.Commands;
using ManagementRPG.Domain.Abstractions.Commands.Updates;
using ManagementRPG.Domain.Global.Campanhas;

namespace ManagementRPG.Application.Global.Campanhas.Commands
{
    public sealed class CampanhaCommandUpdate : CampanhaGeneral, ICommandUpdate<int, int>, ICommandResponse
    {
        public int UserId { get; set; }
        public int Id { get; set; }
    }
}
