using ManagementRPG.Domain.Abstractions.Commands;

namespace ManagementRPG.Application.Global.Campanhas.Commands
{
    public sealed record CampanhaCommandRemove(int id) : ICommandResponse
    {
    }
}
