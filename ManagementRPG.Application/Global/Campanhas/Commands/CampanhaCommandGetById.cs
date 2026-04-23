using ManagementRPG.Domain.Abstractions.Commands;
using ManagementRPG.Domain.Global.Campanhas.Responses;

namespace ManagementRPG.Application.Global.Campanhas.Commands
{
    public sealed record CampanhaCommandGetById(int id) : ICommandResponse<CampanhaResponse>
    {
    }
}
