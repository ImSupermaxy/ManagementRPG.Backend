using System.ComponentModel.DataAnnotations;

namespace ManagementRPG.Domain.Security.Usuarios.Enums
{
    public enum EPerfil
    {
        [Display(Name = "Todos")]
        Todos = -1,

        [Display(Name = "Master")]
        MASTER = -1,

        [Display(Name = "Administrador")]
        ADMINISTRADOR = -1,

        [Display(Name = "Usuário")]
        USUARIO = -1,
    }
}
