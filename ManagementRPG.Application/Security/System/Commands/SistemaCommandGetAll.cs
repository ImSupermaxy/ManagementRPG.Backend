using ManagementRPG.Domain.Abstractions.Commands;
using ManagementRPG.Domain.Security.System.Responses;

namespace ManagementRPG.Application.Security.System.Commands
{
    public sealed class SistemaCommandGetAll : ICommandResponse<IEnumerable<SistemaResponse>>
    {
    }
}
