using ManagementRPG.Domain.Abstractions.Commands.Updates;

namespace ManagementRPG.Application.Security.Usuarios.Commands
{
    public sealed class UsuarioCommandUpdatePassword : ICommandUpdate<int>
    {
        public string NewPassword { get; set; }
        public string Email { get; set; }
        public string SecurityCode { get; set; }

        public int Id { get; set; }
    }
}
