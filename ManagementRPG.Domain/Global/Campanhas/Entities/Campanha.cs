using ManagementRPG.Domain.Abstractions.Entities;
using ManagementRPG.Domain.Shared.Enums;
using ManagementRPG.Domain.Validators.Campanhas;

namespace ManagementRPG.Domain.Global.Campanhas.Entities
{
    public class Campanha : Entity<int, int>
    {
        public int MestreId { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public string Sinopse { get; private set; }

        public Campanha(int userId, int mestreId, string nome, string descricao, string sinopse)
            : base(userId)
        {
            MestreId = mestreId;
            Nome = nome;
            Descricao = descricao;
            Sinopse = sinopse;

            Validate();
        }

        public Campanha(int id, EStatus status, int userInsId, DateTime userInsData, int userModId, DateTime userModData, 
            int mestreId, string nome, string descricao, string sinopse)
            : base(id, status, userInsId, userInsData, userModId, userModData)
        {
            MestreId = mestreId;
            Nome = nome;
            Descricao = descricao;
            Sinopse = sinopse;
        }

        public Campanha(int id, EStatus status, int userInsId, DateTime userInsData, int userModId, string nome, 
            string descricao, string sinopse)
            : base(id, status, userInsId, userInsData, userModId)
        {
            Nome = nome;
            Descricao = descricao;
            Sinopse = sinopse;

            Validate();
        }

        protected override void Validate()
        {
            new CampanhaValidator(this);
        }
    }
}
