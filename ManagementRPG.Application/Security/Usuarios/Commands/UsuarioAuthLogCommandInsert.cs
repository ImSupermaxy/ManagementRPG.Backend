using ManagementRPG.Domain.Abstractions.Commands.Inserts;

namespace ManagementRPG.Application.Security.Usuarios.Commands
{
    public sealed class UsuarioAuthLogCommandInsert : ICommandInsert
    {
        public int UsuarioId { get; set; }
        public string SenhaHash { get; set; }
        public string Token { get; set; }
    }
}
