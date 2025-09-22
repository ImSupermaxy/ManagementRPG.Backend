using ManagementRPG.Application.Security.System.Commands;
using ManagementRPG.Domain.Abstractions.Commands.Handlers;
using ManagementRPG.Domain.Abstractions.Handlers;
using ManagementRPG.Domain.Abstractions.Repositories;
using ManagementRPG.Domain.Security.System.Entities;
using ManagementRPG.Domain.Security.System.Queries;
using ManagementRPG.Domain.Shared.Commands;

namespace ManagementRPG.Application.Security.System.Handlers
{
    public class SistemaHandlerQuery : HandlerQuery<Sistema, SistemaQueryResult, int, int>,
        ICommandHandler<SistemaCommandGetAll, IEnumerable<SistemaQueryResult>>,
        ICommandHandler<SistemaCommandGetById, SistemaQueryResult>
    {
        public SistemaHandlerQuery(IRepository<Sistema, int, int, SistemaQueryResult> repository) 
            : base(repository)
        {
        }

        public async Task<Result<IEnumerable<SistemaQueryResult>>> Handle(SistemaCommandGetAll request, CancellationToken cancellationToken)
        {
            return await HandleGetAll();
        }

        public async Task<Result<SistemaQueryResult>> Handle(SistemaCommandGetById request, CancellationToken cancellationToken)
        {
            return await HandleGet(request.id);
        }
    }
}
