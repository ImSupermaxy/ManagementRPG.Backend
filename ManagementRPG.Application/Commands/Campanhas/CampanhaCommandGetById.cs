using ManagementRPG.Domain.Abstractions.Commands;
using ManagementRPG.Domain.Queries.Campanhas;

namespace ManagementRPG.Application.Commands.Campanhas
{
    public sealed record CampanhaCommandGetById(int id) : ICommandResponse<CampanhaQueryResult>
    {
    }
}
