using ManagementRPG.Domain.Abstractions.Entities;
using ManagementRPG.Domain.Security.Usuarios.Enums;
using ManagementRPG.Domain.Security.Usuarios.Validators;
using ManagementRPG.Domain.Shared.Enums;

namespace ManagementRPG.Domain.Security.Usuarios.Entities
{
    public class Usuario : Entity<int, int>
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Arroba { get; private set; }
        public string Senha { get; private set; }
        public EStatusUsuario Status { get; private set; }
        public IList<EPerfil> Perfis { get; set; }

        public Usuario(int userId, string nome, string email, string arroba, string senha) 
            : base(userId)
        {
            Nome = nome;
            Email = email;
            Arroba = arroba;
            Senha = senha;
            Perfis = new List<EPerfil>() { EPerfil.USUARIO };
            Validate();
        }

        public Usuario(int id, EStatusUsuario status, int userInsId, DateTime userInsData, int userModId, DateTime userModData,
            string nome, string email, string arroba)
            : base(id, userInsId, userInsData, userModId, userModData)
        {
            Nome = nome;
            Email = email;
            Arroba = arroba;
            Status = status;
        }

        public Usuario(int id, EStatusUsuario status, int userInsId, DateTime userInsData, int userModId,
            string nome, string arroba)
            : base(id, userInsId, userInsData, userModId)
        {
            Nome = nome;
            Arroba = arroba;
            Status = status;
            Validate();
        }

        protected override void Validate()
        {
            new UsuarioValidator(this);
        }

        protected override void CreateUserLog(int userId)
        {
            Status = EStatusUsuario.Ativo;
            base.CreateUserLog(userId);
        }

        public void UpdateSenha(string senhaHasher)
        {
            Senha = senhaHasher;
        }

        public void InactiveUsuario()
        {
            Status = EStatusUsuario.Inativo;
        }
    }
}
