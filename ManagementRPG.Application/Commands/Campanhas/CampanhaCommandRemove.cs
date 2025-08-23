using ManagementRPG.Domain.Abstractions.Commands;

namespace ManagementRPG.Application.Commands.Campanhas
{
    public sealed record CampanhaCommandRemove(int id) : ICommandResponse
    {
    }
}
