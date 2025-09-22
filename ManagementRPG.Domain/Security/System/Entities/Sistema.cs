using ManagementRPG.Domain.Abstractions.Entities;
using ManagementRPG.Domain.Shared.Enums;

namespace ManagementRPG.Domain.Security.System.Entities
{
    public class Sistema : Entity<int, int>
    {
        public string Nome { get; private set; }
        public string Versao { get; private set; }

        public Sistema(int userId) : base(userId)
        {
        }

        public Sistema(int userId, string nome, string versao)
            : base(userId)
        {
            Nome = nome;
            Versao = versao;
            Validate();
        }

        public Sistema(int id, EStatus status, int userInsId, DateTime userInsData, int userModId, 
            string nome, string versao) 
            : base(id, status, userInsId, userInsData, userModId)
        {
            Nome = nome;
            Versao = versao;
        }

        public Sistema(int id, EStatus status, int userInsId, DateTime userInsData, int userModId, DateTime userModData,
            string nome, string versao) 
            : base(id, status, userInsId, userInsData, userModId, userModData)
        {
            Nome = nome;
            Versao = versao;
        }

        protected override void Validate()
        {
            //ALTERAR
            UpdateValid();
        }
    }
}
