using ManagementRPG.Application.Security.Usuarios.Commands;
using ManagementRPG.Domain.Abstractions.Mappers;
using ManagementRPG.Domain.Security.Usuarios.Entities;
using ManagementRPG.Domain.Security.Usuarios.Queries;

namespace ManagementRPG.Application.Security.Usuarios.Mappers
{
    public class UsuarioAuthLogMapper : IMapper
    {
        public UsuarioAuthLog GetWrongPassword(UsuarioAuthLogCommandInsert command)
        {
            return new UsuarioAuthLog(command.UsuarioId, false, command.SenhaHash);
        }

        public UsuarioAuthLog GetAuthenticate(UsuarioAuthLogCommandInsert command)
        {
            return new UsuarioAuthLog(command.UsuarioId, true, command.SenhaHash, command.Token);
        }

        public UsuarioAuthLog GetdEntity(UsuarioAuthLogQueryResult command)
        {
            return new UsuarioAuthLog(command.UsuarioId, command.Login, command.Data, command.SenhaHash, command.Token);
        }
    }
}
