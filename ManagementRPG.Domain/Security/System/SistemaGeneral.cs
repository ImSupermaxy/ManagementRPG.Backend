using ManagementRPG.Domain.Security.System.Enums;

namespace ManagementRPG.Domain.Security.System
{
    public abstract class SistemaGeneral
    {
        public string Nome { get; set; }
        public string Versao { get; set; }
        public EStatusSistema Status { get; set; }
    }
}
