using ManagementRPG.Application.Security.Usuarios.Commands;
using ManagementRPG.Domain.Abstractions.Commands.Handlers;
using ManagementRPG.Domain.Abstractions.Handlers;
using ManagementRPG.Domain.Abstractions.Messages.Errors;
using ManagementRPG.Domain.Abstractions.Messages.Successes;
using ManagementRPG.Domain.Security.Usuarios.Entities;
using ManagementRPG.Domain.Security.Usuarios.Repositories;
using ManagementRPG.Domain.Security.Usuarios.Responses;
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
            return Result.Failure<int>(SystemError.GenericError);

            var result = await HandleInsert(request);
            return Result.SuccessChain(result, SuccessMethodTask<UsuarioCommandInsert>.CommandMethod, SuccessTask.GetRunedMethodName());
        }

        public async Task<Result> Handle(UsuarioCommandUpdate request, CancellationToken cancellationToken)
        {
            var result = await HandleUpdate(request);

            return Result.SuccessChain(result, SuccessMethodTask<UsuarioCommandInsert>.CommandMethod, SuccessTask.GetRunedMethodName());
        }
    }
}
