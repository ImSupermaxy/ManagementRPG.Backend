using ManagementRPG.Domain.Abstractions.Commands;
using ManagementRPG.Domain.Global.Campanhas.Queries;

namespace ManagementRPG.Application.Global.Campanhas.Commands
{
    public sealed record CampanhaCommandGetAll() : ICommandResponse<IEnumerable<CampanhaQueryResult>>
    {
    }
}
