using ManagementRPG.Application.Global.Campanhas.Commands;
using ManagementRPG.Domain.Abstractions.Commands.Handlers;
using ManagementRPG.Domain.Abstractions.Handlers;
using ManagementRPG.Domain.Abstractions.Messages.Successes;
using ManagementRPG.Domain.Global.Campanhas.Entities;
using ManagementRPG.Domain.Global.Campanhas.Repositories;
using ManagementRPG.Domain.Global.Campanhas.Responses;
using ManagementRPG.Domain.Shared.Commands;
using ManagementRPG.Domain.Utils;

namespace ManagementRPG.Application.Global.Campanhas.Handlers
{
    public class CampanhaHandlerQuery : HandlerQuery<Campanha, CampanhaResponse, int, int>,
        ICommandHandler<CampanhaCommandGetAll, IEnumerable<CampanhaResponse>>,
        ICommandHandler<CampanhaCommandGetById, CampanhaResponse>
    {
        public CampanhaHandlerQuery(ICampanhaRepository repository) : base(repository)
        {
        }

        public async Task<Result<CampanhaResponse>> Handle(CampanhaCommandGetById request, CancellationToken cancellationToken)
        {
            var result = await HandleGet(request.id);

            return Result.SuccessChain(result, SuccessMethodTask<CampanhaCommandGetById>.CommandMethod, SuccessTask.GetRunedMethodName());
        }

        public async Task<Result<IEnumerable<CampanhaResponse>>> Handle(CampanhaCommandGetAll request, CancellationToken cancellationToken)
        {
            var result = await HandleGetAll();

            return Result.SuccessChain(result, SuccessMethodTask<CampanhaCommandGetAll>.CommandMethod, SuccessTask.GetRunedMethodName());
        }
    }
}
