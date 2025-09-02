namespace ManagementRPG.Domain.Global.Campanhas
{
    public abstract class CampanhaGeneral
    {
        public int MestreId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Sinopse { get; set; }
    }
}
