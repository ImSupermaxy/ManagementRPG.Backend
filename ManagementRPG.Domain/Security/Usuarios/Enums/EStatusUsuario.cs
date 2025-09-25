using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementRPG.Domain.Security.Usuarios.Enums
{
    public enum EStatusUsuario
    {
        [Display(Name = "Todos")]
        Todos = -1,

        [Display(Name = "Inativo")]
        Inativo = 0,

        [Display(Name = "Ativo")]
        Ativo = 1,

        [Display(Name = "Atualizar Senha")]
        AtualizarSenha = 2,

        [Display(Name = "Bloquado")]
        Bloquado = 3,
    }
}
