using ManagementRPG.Application.Security.Usuarios.Commands;
using ManagementRPG.Domain.Abstractions.Commands.Handlers;
using ManagementRPG.Domain.Abstractions.Handlers;
using ManagementRPG.Domain.Abstractions.Mappers;
using ManagementRPG.Domain.Abstractions.Repositories;
using ManagementRPG.Domain.Security.Usuarios.Entities;
using ManagementRPG.Domain.Security.Usuarios.Queries;
using ManagementRPG.Domain.Shared.Commands;

namespace ManagementRPG.Application.Security.Usuarios.Handlers
{
    internal class UsuarioHandler : HandlerEntity<Usuario, int, int, UsuarioCommandInsert, UsuarioCommandUpdate, UsuarioQueryResult>,
        ICommandHandler<UsuarioCommandInsert>,
        ICommandHandler<UsuarioCommandUpdate>
    {
        public UsuarioHandler(IRepository<Usuario, int,int, UsuarioQueryResult> repository, 
            IMapperEntity<Usuario, int, int, UsuarioCommandInsert, UsuarioCommandUpdate, UsuarioQueryResult> mapper) 
            : base(repository, mapper)
        {
        }

        public async Task<Result> Handle(UsuarioCommandInsert request, CancellationToken cancellationToken)
        {
            return await HandleInsert(request);
        }

        public async Task<Result> Handle(UsuarioCommandUpdate request, CancellationToken cancellationToken)
        {
            return await HandleUpdate(request);
        }
    }
}
