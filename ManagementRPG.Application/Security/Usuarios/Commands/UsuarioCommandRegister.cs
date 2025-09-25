using ManagementRPG.Application.Security.Usuarios.Token;
using ManagementRPG.Domain.Abstractions.Commands;
using ManagementRPG.Domain.Abstractions.Commands.Inserts;
using ManagementRPG.Domain.Security.Usuarios;

namespace ManagementRPG.Application.Security.Usuarios.Commands
{
    public sealed class UsuarioCommandRegister : UsuarioGeneral, ICommandResponse<TokenModel>
    {
        public string Password { get; set; }

        public int UserId { get; set; }
    }
}
