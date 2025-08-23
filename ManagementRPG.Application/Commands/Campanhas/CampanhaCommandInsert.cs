using ManagementRPG.Domain.Abstractions.Commands.Inserts;

namespace ManagementRPG.Domain.Commands.Campanhas
{
    public sealed class CampanhaCommandInsert : ICommandInsert<int>
    {
        //CommandInsert do usuário (para o mestre)?
        public int MestreId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Sinopse { get; set; }

        public int UserId { get; set; }
    }
}
