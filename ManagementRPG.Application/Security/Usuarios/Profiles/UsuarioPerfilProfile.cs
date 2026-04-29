using V4MAutoMapper;

namespace ManagementRPG.Application.Security.Usuarios.Profiles
{
    public class UsuarioPerfilProfile : Profile
    {
        protected override void Configure()
        {
            //Passar os DTOS aqui tbm..
            //CreateMap<SistemaQueryResult, Sistema>()
            //     .ConstructUsing(s => { return new Sistema(s.UserId, s.Nome, s.Versao); })
            //     .AfterMap((c, s) => s.Validate());
        }
    }
}
