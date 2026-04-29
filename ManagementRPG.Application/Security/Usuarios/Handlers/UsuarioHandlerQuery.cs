using ManagementRPG.Application.Security.Usuarios.Commands;
using ManagementRPG.Domain.Abstractions.Commands.Handlers;
using ManagementRPG.Domain.Abstractions.Handlers;
using ManagementRPG.Domain.Abstractions.Messages.Successes;
using ManagementRPG.Domain.Security.Usuarios.Entities;
using ManagementRPG.Domain.Security.Usuarios.Repositories;
using ManagementRPG.Domain.Security.Usuarios.Responses;
using ManagementRPG.Domain.Shared.Commands;

namespace ManagementRPG.Application.Security.Usuarios.Handlers
{
    public class UsuarioHandlerQuery : HandlerQuery<Usuario, UsuarioResponse, int>,
        ICommandHandler<UsuarioCommandGetAll, IEnumerable<UsuarioResponse>>
    {
        public UsuarioHandlerQuery(IUsuarioRepository repository) 
            : base(repository)
        {
        }

        public async Task<Result<IEnumerable<UsuarioResponse>>> Handle(UsuarioCommandGetAll request, CancellationToken cancellationToken)
        {
            var result = await HandleGetAll();

            return Result.SuccessChain(result, SuccessMethodTask<UsuarioCommandGetAll>.CommandMethod, SuccessTask.GetRunedMethodName());
        }
    }
}
