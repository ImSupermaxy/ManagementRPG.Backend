using ManagementRPG.Domain.Abstractions.Entities;
using ManagementRPG.Domain.Security.System.Enums;
using ManagementRPG.Domain.Shared.Enums;

namespace ManagementRPG.Domain.Security.System.Entities
{
    public class Sistema : Entity<int, int>
    {
        public string Nome { get; private set; }
        public string Versao { get; private set; }
        public EStatusSistema Status { get; private set; }

        public Sistema(int userId) : base(userId)
        {
        }

        public Sistema(int userId, string nome, string versao)
            : base(userId)
        {
            Nome = nome;
            Versao = versao;
            Status = EStatusSistema.Online;
            Validate();
        }

        public Sistema(int id, EStatusSistema status, int userInsId, DateTime userInsData, int userModId, 
            string nome, string versao) 
            : base(id, userInsId, userInsData, userModId)
        {
            Nome = nome;
            Versao = versao;
            Status = status;
            Validate();
        }

        public Sistema(int id, EStatusSistema status, int userInsId, DateTime userInsData, int userModId, DateTime userModData,
            string nome, string versao) 
            : base(id, userInsId, userInsData, userModId, userModData)
        {
            Nome = nome;
            Versao = versao;
            Status = status;
        }

        protected override void Validate()
        {
            //ALTERAR
            UpdateValid();
        }
    }
}
