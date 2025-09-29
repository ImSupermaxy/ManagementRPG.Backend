using ManagementRPG.Domain.Abstractions.Commands;
using ManagementRPG.Domain.Abstractions.Commands.Inserts;
using ManagementRPG.Domain.Security.Usuarios;
using ManagementRPG.Domain.Security.Usuarios.Enums;

namespace ManagementRPG.Application.Security.Usuarios.Commands
{
    public sealed class UsuarioCommandInsert : UsuarioGeneral, ICommandInsert<int>, ICommandResponse<int>
    {
        public string Senha { get; set; }
        public List<EPerfil> Perfis { get; set; }

        public int UserId { get; set; }
    }
}
