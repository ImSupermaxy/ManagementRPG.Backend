using ManagementRPG.Application.Security.Usuarios.Commands;
using ManagementRPG.Domain.Abstractions.Mappers;
using ManagementRPG.Domain.Security.Usuarios.Entities;
using ManagementRPG.Domain.Security.Usuarios.Queries;

namespace ManagementRPG.Application.Security.Usuarios.Mappers
{
    public class UsuarioMapper : IMapperEntity<Usuario, int, int, UsuarioCommandInsert, UsuarioCommandUpdate, UsuarioQueryResult>
    {
        public Usuario GetEntity(UsuarioCommandInsert command)
        {
            return new Usuario(command.UserId, command.Nome, command.Senha, command.Arroba, command.Senha);
        }

        public Usuario GetEntity(UsuarioQueryResult oldEntity, UsuarioCommandUpdate command)
        {
            return new Usuario(oldEntity.Id, oldEntity.Status, oldEntity.UserInsId, oldEntity.UserInsData, command.UserId,
                command.Nome, command.Arroba);
        }

        public Usuario GetEntity(UsuarioQueryResult command)
        {
            return new Usuario(command.Id, command.Status, command.UserInsId, command.UserInsData, command.UserModId, command.UserModData,
                command.Nome, command.Email, command.Arroba);
        }

        public Usuario GetEntity(UsuarioCommandRegister command)
        {
            return new Usuario(command.UserId, command.Nome, command.Email, command.Arroba, command.Password);
        }
    }
}
