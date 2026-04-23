
using ManagementRPG.Application.Security.System.Commands;
using ManagementRPG.Domain.Security.System.Entities;
using ManagementRPG.Domain.Security.System.Validators;
using V4MAutoMapper;

namespace ManagementRPG.Application.Security.System.Profiles
{
    public class SistemaProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<SistemaCommandInsert, Sistema>()
                 .ConstructUsing(s => 
                    new Sistema(s.UserId, s.Nome, s.Versao))
                .AfterMap((c, e) => 
                    new SistemaValidator(e).ValidateEntity());

            CreateMap<SistemaCommandUpdate, Sistema>()
                 .ConstructUsing(s => new Sistema(s.Id, s.Status, s.UserId, s.Nome, s.Versao))
                 .AfterMap((c, e) =>
                    new SistemaValidator(e).ValidateEntity());

            //Passar os DTOS aqui tbm..
            //CreateMap<SistemaQueryResult, Sistema>()
            //     .ConstructUsing(s => { return new Sistema(s.UserId, s.Nome, s.Versao); })
            //     .AfterMap((c, s) => s.Validate());
        }
    }
}
