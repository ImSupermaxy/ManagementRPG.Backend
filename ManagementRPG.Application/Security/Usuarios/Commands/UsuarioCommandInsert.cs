using ManagementRPG.Domain.Abstractions.Commands.Inserts;
using ManagementRPG.Domain.Security.Usuarios;

namespace ManagementRPG.Application.Security.Usuarios.Commands
{
    public sealed class UsuarioCommandInsert : UsuarioGeneral, ICommandInsert<int>
    {
        public string Senha { get; set; }

        public int UserId { get; set; }
    }
}
