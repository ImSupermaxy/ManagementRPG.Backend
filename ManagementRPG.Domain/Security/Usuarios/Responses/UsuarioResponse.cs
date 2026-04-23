using ManagementRPG.Domain.Abstractions.Responses;
using ManagementRPG.Domain.Security.Usuarios.Enums;

namespace ManagementRPG.Domain.Security.Usuarios.Responses
{
    public sealed class UsuarioResponse : UsuarioGeneral, IResponse<int, int>
    {
        public string SenhaHash { get; set; }
        public List<EPerfil> Perfis { get; set; }

        public int UserInsId { get; set; }
        public DateTime UserInsData { get; set; }
        public int UserModId { get; set; }
        public DateTime UserModData { get; set; }

        public int Id { get; set; }
    }
}
