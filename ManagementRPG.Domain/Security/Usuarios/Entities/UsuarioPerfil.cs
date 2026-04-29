using ManagementRPG.Domain.Security.Usuarios.Enums;

namespace ManagementRPG.Domain.Security.Usuarios.Entities
{
    public class UsuarioPerfil
    {
        public int UsuarioId { get; private set; }
        public int SistemaId { get; private set; }
        public EPerfil Perfil { get; private set; }

        public UsuarioPerfil()
        {
            Perfil = EPerfil.USUARIO;
        }

        public UsuarioPerfil(int usuarioId, int sistemaId, EPerfil perfil)
        {
            UsuarioId = usuarioId;
            SistemaId = sistemaId;
            Perfil = perfil;
        }

        public UsuarioPerfil[] GetByPerfis(int usuarioId, int sistemaId, EPerfil[] perfis) => perfis.Select(p => new UsuarioPerfil(usuarioId, sistemaId, p)).ToArray();

        public static EPerfil[] GetDefaultPerfil => [EPerfil.USUARIO];
        public static UsuarioPerfil[] GetDefaultEntity(int usuarioId, int sistemaId) => GetDefaultPerfil.Select(p => new UsuarioPerfil(usuarioId, sistemaId, p)).ToArray();
    }
}
