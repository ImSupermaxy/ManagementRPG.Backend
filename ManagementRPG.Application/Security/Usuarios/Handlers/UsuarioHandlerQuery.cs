using ManagementRPG.Application.Security.Usuarios.Commands;
using ManagementRPG.Domain.Abstractions.Commands.Handlers;
using ManagementRPG.Domain.Abstractions.Errors;
using ManagementRPG.Domain.Abstractions.Handlers;
using ManagementRPG.Domain.Abstractions.Repositories;
using ManagementRPG.Domain.Security.Usuarios.Entities;
using ManagementRPG.Domain.Security.Usuarios.Queries;
using ManagementRPG.Domain.Security.Usuarios.Repositories;
using ManagementRPG.Domain.Shared.Commands;

namespace ManagementRPG.Application.Security.Usuarios.Handlers
{
    public class UsuarioHandlerQuery : HandlerQuery<Usuario, UsuarioQueryResult, int>,
        ICommandHandler<UsuarioCommandGetAll, IEnumerable<UsuarioQueryResult>>
    {
        public UsuarioHandlerQuery(IUsuarioRepository repository) 
            : base(repository)
        {
        }

        public async Task<Result<IEnumerable<UsuarioQueryResult>>> Handle(UsuarioCommandGetAll request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await Repository.Get();
                return Result.Success(result);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<UsuarioQueryResult>>(SystemError.GenericError, ex.Message);
            }
        }
    }
}
