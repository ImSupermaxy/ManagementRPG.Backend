using ManagementRPG.Domain.Abstractions.Entities;
using ManagementRPG.Domain.Security.System.Enums;

namespace ManagementRPG.Domain.Security.System.Entities
{
    public class Sistema : Entity<int, int>
    {
        public string Nome { get; private set; }
        public string Versao { get; private set; }
        public EStatusSistema Status { get; private set; }

        //Insert
        public Sistema(int userId, string nome, string versao)
            : base(userId)
        {
            Nome = nome;
            Versao = versao;
            Status = EStatusSistema.Online;
        }

        //Update
        public Sistema(int id, EStatusSistema status, int userId, 
            string nome, string versao) 
            : base(id, userId)
        {
            Nome = nome;
            Versao = versao;
            Status = status;
        }
    }
}
