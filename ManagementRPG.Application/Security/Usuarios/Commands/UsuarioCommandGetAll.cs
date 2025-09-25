using ManagementRPG.Domain.Abstractions.Commands;
using ManagementRPG.Domain.Security.Usuarios.Queries;

namespace ManagementRPG.Application.Security.Usuarios.Commands
{
    public sealed class UsuarioCommandGetAll : ICommandResponse<IEnumerable<UsuarioQueryResult>>
    {
    }
}
