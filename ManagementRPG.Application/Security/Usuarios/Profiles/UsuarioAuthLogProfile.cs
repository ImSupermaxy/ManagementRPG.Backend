using ManagementRPG.Application.Security.Usuarios.Commands;
using ManagementRPG.Domain.Security.Usuarios.Entities;
using V4MAutoMapper;

namespace ManagementRPG.Application.Security.Usuarios.Profiles
{
    public class UsuarioAuthLogProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<UsuarioAuthLogCommandInsert, UsuarioAuthLog>()
                 .ConstructUsing(c =>
                    new UsuarioAuthLog(c.UsuarioId, c.Login, c.SenhaHash, c.Token));

            //Passar os DTOS aqui tbm..
            //CreateMap<SistemaQueryResult, Sistema>()
            //     .ConstructUsing(s => { return new Sistema(s.UserId, s.Nome, s.Versao); })
            //     .AfterMap((c, s) => s.Validate());
        }
    }
}
