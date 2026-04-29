using ManagementRPG.Domain.Abstractions.Entities;
using ManagementRPG.Domain.Security.Usuarios.Enums;

namespace ManagementRPG.Domain.Security.Usuarios.Entities
{
    public class Usuario : Entity<int, int>
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Arroba { get; private set; }
        public string SenhaHash { get; private set; }
        public EStatusUsuario Status { get; private set; }
        //public IList<EPerfil> Perfis { get; set; }

        //Insert Admin
        public Usuario(int userId, string nome, string email, string arroba, string senha) 
            : base(userId)
        {
            Nome = nome;
            Email = email;
            Arroba = arroba;
            SenhaHash = senha;
            Status = EStatusUsuario.Ativo;
            //Perfis = new List<EPerfil>() { EPerfil.USUARIO };
        }

        //Update Admin
        public Usuario(int id, int userId, string nome, string email, string arroba)
            : base(id, userId)
        {
            Nome = nome;
            Email = email;
            Arroba = arroba;
        }

        protected override void CreateUserLog(int userId)
        {
            Status = EStatusUsuario.Ativo;
            base.CreateUserLog(userId);
        }

        public void UpdateSenha(string senhaHasher)
        {
            SenhaHash = senhaHasher;
        }

        public void InactiveUsuario()
        {
            Status = EStatusUsuario.Inativo;
        }
    }
}
