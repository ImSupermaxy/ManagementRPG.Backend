using ManagementRPG.Application.Security.Usuarios.Commands;
using ManagementRPG.Domain.Abstractions.Commands.Handlers;
using ManagementRPG.Domain.Abstractions.Handlers;
using ManagementRPG.Domain.Security.Usuarios.Entities;
using ManagementRPG.Domain.Security.Usuarios.Responses;
using ManagementRPG.Domain.Security.Usuarios.Repositories;
using ManagementRPG.Domain.Shared.Commands;
using V4MAutoMapper;

namespace ManagementRPG.Application.Security.Usuarios.Handlers
{
    public class UsuarioHandler : HandlerEntity<Usuario, int, int, UsuarioCommandInsert, UsuarioCommandUpdate, UsuarioResponse>,
        ICommandHandler<UsuarioCommandInsert, int>,
        ICommandHandler<UsuarioCommandUpdate>
    {
        public UsuarioHandler(IUsuarioRepository repository, IMapper mapper) 
            : base(repository, mapper)
        {
        }

        public async Task<Result<int>> Handle(UsuarioCommandInsert request, CancellationToken cancellationToken)
        {
            return await HandleInsert(request);
        }

        public async Task<Result> Handle(UsuarioCommandUpdate request, CancellationToken cancellationToken)
        {
            return await HandleUpdate(request);
        }
    }
}
