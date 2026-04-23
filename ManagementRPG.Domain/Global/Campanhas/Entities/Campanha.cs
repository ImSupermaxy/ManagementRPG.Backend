using ManagementRPG.Domain.Abstractions.Entities;
using ManagementRPG.Domain.Shared.Enums;

namespace ManagementRPG.Domain.Global.Campanhas.Entities
{
    public class Campanha : EntityDefault<int, int>
    {
        public int MestreId { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public string Sinopse { get; private set; }

        //Insert
        public Campanha(int userId, int mestreId, string nome, string descricao, string sinopse)
            : base(userId)
        {
            MestreId = mestreId;
            Nome = nome;
            Descricao = descricao;
            Sinopse = sinopse;
        }

        //Update
        public Campanha(int id, EStatus status, int userId, int mestreId, string nome, string descricao, string sinopse)
            : base(id, userId, status)
        {
            MestreId = mestreId;
            Nome = nome;
            Descricao = descricao;
            Sinopse = sinopse;
        }
    }
}
