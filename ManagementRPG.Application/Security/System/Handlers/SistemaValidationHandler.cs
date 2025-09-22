using ManagementRPG.Application.Security.System.Commands;
using ManagementRPG.Domain.Abstractions.Errors;
using ManagementRPG.Domain.Abstractions.Handlers;
using ManagementRPG.Domain.Security.System.Repositories;
using ManagementRPG.Domain.Shared.ApiConfig;
using ManagementRPG.Domain.Shared.Commands;

namespace ManagementRPG.Application.Security.System.Handlers
{
    internal class SistemaValidationHandler : IHandler
    {
        protected ISistemaRepository Repository { get; set; }
        //TODO: Fazer uma interface genêrica tipo o IAppSettings para obter das variaveis de ambiente o sistema atual
        //  (pra não precisar ficar indo na base)
        //protected ISystemConfig SystemConfig { get; set; }

        public SistemaValidationHandler(ISistemaRepository repository/*, ISystemConfig SystemConfig*/)

        {
        }

        public async Task<Result> Handle(SistemaCommandGetValidation request, CancellationToken cancellationToken)
        {
            var sistema = await Repository.GetLastSistema();

            if (sistema != null)
                return Result.Failure(SystemError.GenericError);

            //if (SystemConfig.System != sistema)
            //    return Result.Failure(SystemError.Deprecated);
            
            return Result.Success();
        }
    }
}
