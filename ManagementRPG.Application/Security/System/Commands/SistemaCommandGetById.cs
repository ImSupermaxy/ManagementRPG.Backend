using ManagementRPG.Domain.Abstractions.Commands;
using ManagementRPG.Domain.Security.System.Queries;

namespace ManagementRPG.Application.Security.System.Commands
{
    public sealed record class SistemaCommandGetById(int id) : ICommandResponse<SistemaQueryResult>
    {
    }
}
