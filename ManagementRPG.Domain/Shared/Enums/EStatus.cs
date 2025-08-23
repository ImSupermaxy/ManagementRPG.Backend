using System.ComponentModel.DataAnnotations;

namespace ManagementRPG.Domain.Shared.Enums
{
    public enum EStatus
    {
        [Display(Name = "Todos")]
        Todos = -1,

        [Display(Name = "Inativo")]
        Inativo = 0,

        [Display(Name = "Ativo")]
        Ativo = 1
    }
}
