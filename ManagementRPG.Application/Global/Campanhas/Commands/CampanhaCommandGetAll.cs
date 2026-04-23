using ManagementRPG.Domain.Abstractions.Commands;
using ManagementRPG.Domain.Global.Campanhas.Responses;

namespace ManagementRPG.Application.Global.Campanhas.Commands
{
    public sealed record CampanhaCommandGetAll() : ICommandResponse<IEnumerable<CampanhaResponse>>
    {
    }
}
