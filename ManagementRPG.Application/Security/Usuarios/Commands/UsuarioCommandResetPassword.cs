using ManagementRPG.Domain.Abstractions.Commands;

namespace ManagementRPG.Application.Security.Usuarios.Commands
{
    public sealed class UsuarioCommandResetPassword : ICommandResponse
    {
        public string Email { get; set; }
    }
}
