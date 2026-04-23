using ManagementRPG.Domain.Abstractions.Handlers;
using ManagementRPG.Domain.Abstractions.Commands.Handlers;
using ManagementRPG.Domain.Shared.Commands;
using ManagementRPG.Application.Global.Campanhas.Commands;
using ManagementRPG.Domain.Global.Campanhas.Responses;
using ManagementRPG.Domain.Global.Campanhas.Repositories;
using ManagementRPG.Domain.Global.Campanhas.Entities;
using V4MAutoMapper;

namespace ManagementRPG.Application.Global.Campanhas.Handlers
{
    public sealed class CampanhaHandler : HandlerEntity<Campanha, int, int, CampanhaCommandInsert, CampanhaCommandUpdate, CampanhaResponse>,
        ICommandHandler<CampanhaCommandInsert, int>,
        ICommandHandler<CampanhaCommandUpdate>
        //,ICommandHandler<CampanhaCommandRemove>
    {

        public CampanhaHandler(ICampanhaRepository repository, IMapper mapper) 
            : base(repository, mapper)
        {
        }

        public async Task<Result<int>> Handle(CampanhaCommandInsert request, CancellationToken cancellationToken)
        {
            return await HandleInsert(request);
        }

        public async Task<Result> Handle(CampanhaCommandUpdate request, CancellationToken cancellationToken)
        {
            return await HandleUpdate(request);
        }

        //public async Task<CommandResult> Handle(CampanhaCommandRemove request, CancellationToken cancellationToken)
        //{
        //    return await HandleRemove(request.id);
        //}
    }
}
