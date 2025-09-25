using ManagementRPG.Domain.Abstractions.Commands;
using ManagementRPG.Domain.Shared.Commands;


namespace ManagementRPG.Application.Security.System.Commands
{
    internal sealed record class SistemaCommandGetValidation() : ICommandResponse<int>
    {
    }
}
