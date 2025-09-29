using ManagementRPG.Application.Security.System.Commands;
using ManagementRPG.Domain.Abstractions.Mappers;
using ManagementRPG.Domain.Security.System.Entities;
using ManagementRPG.Domain.Security.System.Queries;

namespace ManagementRPG.Application.Security.System.Mappers
{
    public class SistemaMapper : IMapperEntity<Sistema, int, int, SistemaCommandInsert, SistemaCommandUpdate, SistemaQueryResult>
    {
        public Sistema GetEntity(SistemaCommandInsert command)
        {
            return new Sistema(command.UserId, command.Nome, command.Versao);
        }

        public Sistema GetEntity(SistemaQueryResult oldEntity, SistemaCommandUpdate command)
        {
            return new Sistema(command.Id, command.Status, oldEntity.UserInsId, oldEntity.UserInsData, command.UserId, 
                command.Nome, command.Versao);
        }

        public Sistema GetEntity(SistemaQueryResult command)
        {
            return new Sistema(command.Id, command.Status, command.UserInsId, command.UserInsData, command.UserModId, command.UserModData,
                command.Nome, command.Versao);
        }
    }
}
