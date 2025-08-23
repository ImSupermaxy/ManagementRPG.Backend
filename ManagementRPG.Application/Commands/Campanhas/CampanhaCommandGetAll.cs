using ManagementRPG.Domain.Abstractions.Commands;
using ManagementRPG.Domain.Queries.Campanhas;

namespace ManagementRPG.Application.Commands.Campanhas
{
    public sealed record CampanhaCommandGetAll() : ICommandResponse<IEnumerable<CampanhaQueryResult>>
    {
    }
}
