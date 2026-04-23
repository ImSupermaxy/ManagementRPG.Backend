using ManagementRPG.Application.Global.Campanhas.Commands;
using ManagementRPG.Domain.Global.Campanhas.Entities;
using ManagementRPG.Domain.Global.Campanhas.Validators;
using V4MAutoMapper;

namespace ManagementRPG.Application.Global.Campanhas.Profiles
{
    public class CampanhaProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<CampanhaCommandInsert, Campanha>()
                 .ConstructUsing(c =>
                    new Campanha(c.UserId, c.MestreId, c.Nome, c.Descricao, c.Sinopse))
                .AfterMap((c, e) =>
                    new CampanhaValidator(e).ValidateEntity());

            CreateMap<CampanhaCommandUpdate, Campanha>()
                 .ConstructUsing(c => new Campanha(c.Id, c.Status, c.UserId, c.MestreId, c.Nome, c.Descricao, c.Sinopse))
                 .AfterMap((c, e) =>
                    new CampanhaValidator(e).ValidateEntity());

            //Passar os DTOS aqui tbm..
            //CreateMap<SistemaQueryResult, Sistema>()
            //     .ConstructUsing(s => { return new Sistema(s.UserId, s.Nome, s.Versao); })
            //     .AfterMap((c, s) => s.Validate());
        }
    }
}
