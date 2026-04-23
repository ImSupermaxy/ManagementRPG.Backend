using ManagementRPG.Domain.Abstractions.Commands;

namespace ManagementRPG.Domain.Security.Usuarios.Responses
{
    public class UsuarioAuthLogResponse : ICommand
    {
        public int UsuarioId { get; set; }
        public bool Login { get; set; }
        public DateTime Data { get; set; }
        public string SenhaHash { get; set; }
        public string Token { get; set; }
    }
}
