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
            //return new Sistema(command.Id, command.Status, command.UserInserId command.Nome, command.Versao);
            throw new NotImplementedException();
        }

        public Sistema GetEntity(SistemaQueryResult command)
        {
            throw new NotImplementedException();
        }
    }
}
