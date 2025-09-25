using ManagementRPG.Domain.Security.Usuarios.Enums;

namespace ManagementRPG.Domain.Security.Usuarios
{
    public abstract class UsuarioGeneral
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Arroba { get; set; }
    }
}
