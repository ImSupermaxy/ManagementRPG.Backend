using ManagementRPG.Application.Security.System.Commands;
using ManagementRPG.Domain.Abstractions.Commands.Handlers;
using ManagementRPG.Domain.Abstractions.Handlers;
using ManagementRPG.Domain.Security.System.Entities;
using ManagementRPG.Domain.Security.System.Responses;
using ManagementRPG.Domain.Security.System.Repositories;
using ManagementRPG.Domain.Shared.Commands;
using V4MAutoMapper;

namespace ManagementRPG.Application.Security.System.Handlers
{
    public class SistemaHandler : HandlerEntity<Sistema, int, int, SistemaCommandInsert, SistemaCommandUpdate, SistemaResponse>,
        ICommandHandler<SistemaCommandInsert, int>,
        ICommandHandler<SistemaCommandUpdate>
    {
        public SistemaHandler(ISistemaRepository repository, IMapper mapper) 
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
