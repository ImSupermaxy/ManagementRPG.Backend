using ManagementRPG.Domain.Abstractions.Entities;

namespace ManagementRPG.Domain.Security.Usuarios.Entities
{
    public class UsuarioAuthLog : IEntity
    {
        public int UsuarioId { get; private init; }
        public bool Login { get; private init; }
        public DateTime Data { get; private init; }
        public string SenhaHash { get; private init; }
        public string Token { get; private init; }

        public UsuarioAuthLog(int usuarioId, bool login, string senhaHash, string token = null!)
        {
            UsuarioId = usuarioId;
            Login = login;
            Data = DateTime.Now;
            SenhaHash = senhaHash;
            Token = login ? token : null!;
        }

        public UsuarioAuthLog(int usuarioId, bool login, DateTime data, string senhaHash, string token)
        {
            UsuarioId = usuarioId;
            Login = login;
            Data = data;
            SenhaHash = senhaHash;
            Token = token;
        }
    }
}
