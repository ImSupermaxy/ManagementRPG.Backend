using ManagementRPG.Domain.Abstractions.Queries;

namespace ManagementRPG.Domain.Security.Usuarios.Queries
{
    public class UsuarioAuthLogQueryResult : IQuery
    {
        public int UsuarioId { get; set; }
        public bool Login { get; set; }
        public DateTime Data { get; set; }
        public string SenhaHash { get; set; }
        public string Token { get; set; }
    }
}
