using ManagementRPG.Domain.Commands.Campanhas;
using ManagementRPG.Domain.Queries.Campanhas;
using ManagementRPG.Domain.Abstractions.Mappers;
using ManagementRPG.Domain.Entities.Campanhas;

namespace ManagementRPG.Domain.Mappers.Campanhas
{
    public class CampanhaMapper : IMapperEntity<Campanha, int, int, CampanhaCommandInsert, CampanhaCommandUpdate, CampanhaQueryResult>
    {
        public Campanha GetEntity(CampanhaCommandInsert command)
        {
            return new Campanha(command.UserId, command.MestreId, command.Nome, command.Descricao, command.Sinopse);
        }

        public Campanha GetEntity(CampanhaQueryResult oldEntity, CampanhaCommandUpdate command)
        {
            return new Campanha(oldEntity.Id, oldEntity.Status, oldEntity.UserInsId, oldEntity.UserInsData, 
                command.UserId, command.Nome, command.Descricao, command.Sinopse);
        }

        public Campanha GetEntity(CampanhaQueryResult command)
        {
            return new Campanha(command.Id, command.Status, command.UserInsId, command.UserInsData,
                command.UserModId, command.UserModData, command.MestreId, command.Nome, command.Descricao, command.Sinopse);
        }
    }
}
