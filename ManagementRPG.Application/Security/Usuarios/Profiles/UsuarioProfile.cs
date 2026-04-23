using ManagementRPG.Application.Security.Usuarios.Commands;
using ManagementRPG.Domain.Security.Usuarios.Entities;
using ManagementRPG.Domain.Security.Usuarios.Responses;
using ManagementRPG.Domain.Security.Usuarios.Validators;
using V4MAutoMapper;

namespace ManagementRPG.Application.Security.Usuarios.Profiles
{
    public class UsuarioProfile : Profile
    {
        protected override void Configure()
        {
            //Defaults...
            CreateMap<UsuarioCommandInsert, Usuario>()
                 .ConstructUsing(c =>
                    new Usuario(c.UserId, c.Nome, c.Email, c.Arroba, c.Senha))
                .AfterMap((c, e) =>
                    new UsuarioValidator(e).ValidateEntity());

            CreateMap<UsuarioCommandUpdate, Usuario>()
                 .ConstructUsing(c => new Usuario(c.Id, c.UserId, c.Nome, c.Email, c.Arroba))
                 .AfterMap((c, e) =>
                    new UsuarioValidator(e).ValidateEntity());

            CreateMap<UsuarioCommandRegister, Usuario>()
                 .ConstructUsing(c =>
                    new Usuario(c.UserId, c.Nome, c.Email, c.Arroba, c.Password))
                .AfterMap((c, e) =>
                    new UsuarioValidator(e).ValidateEntity());

            //Passar os DTOS aqui tbm..
            //CreateMap<SistemaQueryResult, Sistema>()
            //     .ConstructUsing(s => { return new Sistema(s.UserId, s.Nome, s.Versao); })
            //     .AfterMap((c, s) => s.Validate());

            //Customs...
            CreateMap<UsuarioResponse, UsuarioCommandUpdate>()
                .ForMember(d => d.Nome, opt => opt.MapFrom(r => r.Nome))
                .ForMember(d => d.Email, opt => opt.MapFrom(r => r.Email))
                .ForMember(d => d.Arroba, opt => opt.MapFrom(r => r.Arroba))
                .ForMember(d => d.Status, opt => opt.MapFrom(r => r.Status));
        }
    }
}
