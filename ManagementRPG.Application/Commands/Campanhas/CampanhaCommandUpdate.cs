using ManagementRPG.Domain.Abstractions.Commands.Updates;

namespace ManagementRPG.Domain.Commands.Campanhas
{
    public sealed class CampanhaCommandUpdate : ICommandUpdate<int, int>
    {
        public int MestreId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Sinopse { get; set; }

        public int UserId { get; set; }
        public int Id { get; set; }
    }
}
