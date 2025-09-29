using ManagementRPG.Domain.Abstractions.Commands;
using ManagementRPG.Domain.Abstractions.Commands.Updates;
using ManagementRPG.Domain.Security.Usuarios;

namespace ManagementRPG.Application.Security.Usuarios.Commands
{
    public sealed class UsuarioCommandUpdate : UsuarioGeneral, ICommandUpdate<int, int>, ICommandResponse
    {
        public int UserId { get; set; }

        public int Id { get; set; }
    }
}
