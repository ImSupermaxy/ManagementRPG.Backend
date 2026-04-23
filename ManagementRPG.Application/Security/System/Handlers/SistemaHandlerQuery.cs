using ManagementRPG.Application.Security.System.Commands;
using ManagementRPG.Domain.Abstractions.Commands.Handlers;
using ManagementRPG.Domain.Abstractions.Handlers;
using ManagementRPG.Domain.Security.System.Entities;
using ManagementRPG.Domain.Security.System.Responses;
using ManagementRPG.Domain.Security.System.Repositories;
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
            return await HandleGetAll();
        }

        public async Task<Result<SistemaResponse>> Handle(SistemaCommandGetById request, CancellationToken cancellationToken)
        {
            return await HandleGet(request.id);
        }
    }
}
