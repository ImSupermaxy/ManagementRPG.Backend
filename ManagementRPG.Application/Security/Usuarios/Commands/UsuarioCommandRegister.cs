using ManagementRPG.Domain.Abstractions.Commands;
using ManagementRPG.Domain.Abstractions.Commands.Inserts;
using ManagementRPG.Domain.Security.Usuarios;

namespace ManagementRPG.Application.Security.Usuarios.Commands
{
    public sealed class UsuarioCommandRegister : UsuarioGeneral, ICommandResponse<object>
    {
        public string Password { get; set; }

        public int UserId { get; set; }
    }
}
