using ManagementRPG.Domain.Abstractions.Queries.Results;
using ManagementRPG.Domain.Security.Usuarios.Enums;
using ManagementRPG.Domain.Shared.Enums;

namespace ManagementRPG.Domain.Security.Usuarios.Queries
{
    public sealed class UsuarioQueryResult : UsuarioGeneral, IQueryResult<int, int>
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
