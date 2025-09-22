using ManagementRPG.Application.Global.Campanhas.Commands;
using ManagementRPG.Domain.Abstractions.Commands.Handlers;
using ManagementRPG.Domain.Abstractions.Handlers;
using ManagementRPG.Domain.Abstractions.Repositories;
using ManagementRPG.Domain.Global.Campanhas.Entities;
using ManagementRPG.Domain.Global.Campanhas.Queries;
using ManagementRPG.Domain.Shared.Commands;

namespace ManagementRPG.Application.Global.Campanhas.Handlers
{
    public class CampanhaHandlerQuery : HandlerQuery<Campanha, CampanhaQueryResult, int, int>,
        ICommandHandler<CampanhaCommandGetAll, IEnumerable<CampanhaQueryResult>>,
        ICommandHandler<CampanhaCommandGetById, CampanhaQueryResult>
    {
        public CampanhaHandlerQuery(IRepository<Campanha, int, int, CampanhaQueryResult> repository) : base(repository)
        {
        }

        public async Task<Result<CampanhaQueryResult>> Handle(CampanhaCommandGetById request, CancellationToken cancellationToken)
        {
            return await HandleGet(request.id);
        }

        public async Task<Result<IEnumerable<CampanhaQueryResult>>> Handle(CampanhaCommandGetAll request, CancellationToken cancellationToken)
        {
            return await HandleGetAll();
        }
    }
}
