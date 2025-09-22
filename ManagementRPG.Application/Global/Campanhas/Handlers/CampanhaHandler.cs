using ManagementRPG.Domain.Abstractions.Handlers;
using ManagementRPG.Domain.Abstractions.Commands.Handlers;
using ManagementRPG.Domain.Shared.Commands;
using ManagementRPG.Application.Global.Campanhas.Commands;
using ManagementRPG.Application.Global.Campanhas.Mappers;
using ManagementRPG.Domain.Global.Campanhas.Queries;
using ManagementRPG.Domain.Global.Campanhas.Repositories;
using ManagementRPG.Domain.Global.Campanhas.Entities;

namespace ManagementRPG.Application.Global.Campanhas.Handlers
{
    public sealed class CampanhaHandler : HandlerEntity<Campanha, int, int, CampanhaCommandInsert, CampanhaCommandUpdate, CampanhaQueryResult>,
        ICommandHandler<CampanhaCommandInsert>,
        ICommandHandler<CampanhaCommandUpdate>
        //,ICommandHandler<CampanhaCommandRemove>
    {

        public CampanhaHandler(ICampanhaRepository repository, CampanhaMapper mapper) 
            : base(repository, mapper)
        {
        }

        public async Task<Result> Handle(CampanhaCommandInsert request, CancellationToken cancellationToken)
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
