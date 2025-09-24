using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementRPG.Domain.Security.System.Enums
{
    public enum EStatusSistema
    {
        [Display(Name = "Todos")]
        Todos = -1,

        [Display(Name = "Offline")]
        Offline = 0,

        [Display(Name = "Online")]
        Online = 1,

        [Display(Name = "Manutenção")]
        maintenance = 2,

        [Display(Name = "Fechado")]
        Closed = 3,
    }
}
