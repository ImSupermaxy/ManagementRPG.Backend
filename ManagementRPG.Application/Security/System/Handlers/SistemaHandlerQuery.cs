using ManagementRPG.Application.Security.System.Commands;
using ManagementRPG.Domain.Abstractions.Commands.Handlers;
using ManagementRPG.Domain.Abstractions.Handlers;
using ManagementRPG.Domain.Abstractions.Messages.Successes;
using ManagementRPG.Domain.Security.System.Entities;
using ManagementRPG.Domain.Security.System.Repositories;
using ManagementRPG.Domain.Security.System.Responses;
using ManagementRPG.Domain.Shared.Commands;

namespace ManagementRPG.Application.Security.System.Handlers
{
    public class SistemaHandlerQuery : HandlerQuery<Sistema, SistemaResponse, int, int>,
        ICommandHandler<SistemaCommandGetAll, IEnumerable<SistemaResponse>>,
        ICommandHandler<SistemaCommandGetById, SistemaResponse>
    {
        public SistemaHandlerQuery(ISistemaRepository repository) 
            : base(repository)
        {
        }

        public async Task<Result<IEnumerable<SistemaResponse>>> Handle(SistemaCommandGetAll request, CancellationToken cancellationToken)
        {
            var result = await HandleGetAll();

            return Result.SuccessChain(result, SuccessMethodTask<SistemaCommandGetAll>.CommandMethod, SuccessTask.GetRunedMethodName());
        }

        public async Task<Result<SistemaResponse>> Handle(SistemaCommandGetById request, CancellationToken cancellationToken)
        {
            var result = await HandleGet(request.id);

            return Result.SuccessChain(result, SuccessMethodTask<SistemaCommandGetAll>.CommandMethod, SuccessTask.GetRunedMethodName());
        }
    }
}
