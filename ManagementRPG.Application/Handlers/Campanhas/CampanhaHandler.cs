using ManagementRPG.Domain.Commands.Campanhas;
using ManagementRPG.Domain.Queries.Campanhas;
using ManagementRPG.Domain.Abstractions.Handlers;
using ManagementRPG.Domain.Entities.Campanhas;
using ManagementRPG.Domain.Abstractions.Commands.Handlers;
using ManagementRPG.Domain.Shared.Commands;
using ManagementRPG.Application.Commands.Campanhas;
using ManagementRPG.Domain.Repositories.Campanhas;
using ManagementRPG.Domain.Mappers.Campanhas;

namespace ManagementRPG.Application.Handlers.Campanhas
{
    internal sealed class CampanhaHandler : HandlerEntity<Campanha, int, int, CampanhaCommandInsert, CampanhaCommandUpdate, CampanhaQueryResult>,
        ICommandHandler<CampanhaCommandGetAll, IEnumerable<CampanhaQueryResult>>,
        ICommandHandler<CampanhaCommandGetById, CampanhaQueryResult>,
        ICommandHandler<CampanhaCommandInsert>,
        ICommandHandler<CampanhaCommandUpdate>,
        ICommandHandler<CampanhaCommandRemove>
    {

        public CampanhaHandler(ICampanhaRepository repository, CampanhaMapper mapper) 
            : base(repository, mapper)
        {
        }

        public async Task<CommandResult<IEnumerable<CampanhaQueryResult>>> Handle(CampanhaCommandGetAll request, CancellationToken cancellationToken)
        {
            return await HandleGetAll();
        }

        public async Task<CommandResult<CampanhaQueryResult>> Handle(CampanhaCommandGetById request, CancellationToken cancellationToken)
        {
            return await HandleGet(request.id);
        }

        public async Task<CommandResult> Handle(CampanhaCommandInsert request, CancellationToken cancellationToken)
        {
            return await HandleInsert(request);
        }

        public async Task<CommandResult> Handle(CampanhaCommandUpdate request, CancellationToken cancellationToken)
        {
            return await HandleUpdate(request);
        }

        public async Task<CommandResult> Handle(CampanhaCommandRemove request, CancellationToken cancellationToken)
        {
            return await HandleRemove(request.id);
        }
    }
}
