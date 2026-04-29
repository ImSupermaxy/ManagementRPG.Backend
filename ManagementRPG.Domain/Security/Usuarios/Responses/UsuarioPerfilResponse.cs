using ManagementRPG.Domain.Abstractions.Commands;
using ManagementRPG.Domain.Security.Usuarios.Enums;

namespace ManagementRPG.Domain.Security.Usuarios.Responses
{
    public class UsuarioPerfilResponse : ICommand
    {
        public int UsuarioId { get; set; }
        public int SistemaId { get; set; }
        public EPerfil Perfil { get; set; }
    }
}
