using ManagementRPG.Application.Security.System.Commands;
using ManagementRPG.Application.Security.System.Mappers;
using ManagementRPG.Domain.Abstractions.Commands.Handlers;
using ManagementRPG.Domain.Abstractions.Handlers;
using ManagementRPG.Domain.Abstractions.Mappers;
using ManagementRPG.Domain.Abstractions.Repositories;
using ManagementRPG.Domain.Security.System.Entities;
using ManagementRPG.Domain.Security.System.Queries;
using ManagementRPG.Domain.Security.System.Repositories;
using ManagementRPG.Domain.Shared.Commands;

namespace ManagementRPG.Application.Security.System.Handlers
{
    public class SistemaHandler : HandlerEntity<Sistema, int, int, SistemaCommandInsert, SistemaCommandUpdate, SistemaQueryResult>,
        ICommandHandler<SistemaCommandInsert, int>,
        ICommandHandler<SistemaCommandUpdate>
    {
        public SistemaHandler(ISistemaRepository repository,
            SistemaMapper mapper) 
            : base(repository, mapper)
        {
        }

        public async Task<Result<int>> Handle(SistemaCommandInsert request, CancellationToken cancellationToken)
        {
            return await HandleInsert(request);
        }

        public async Task<Result> Handle(SistemaCommandUpdate request, CancellationToken cancellationToken)
        {
            return await HandleUpdate(request);
        }
    }
}
