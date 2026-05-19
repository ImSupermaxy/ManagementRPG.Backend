using ManagementRPG.Application.Global.Campanhas.Commands;
using ManagementRPG.Application.Global.Campanhas.DTOs;
using ManagementRPG.Domain.Abstractions.Commands.Handlers;
using ManagementRPG.Domain.Abstractions.Handlers;
using ManagementRPG.Domain.Abstractions.Messages.Errors;
using ManagementRPG.Domain.Abstractions.Messages.Successes;
using ManagementRPG.Domain.Global.Campanhas.Entities;
using ManagementRPG.Domain.Global.Campanhas.Repositories;
using ManagementRPG.Domain.Global.Campanhas.Responses;
using ManagementRPG.Domain.Shared.Commands;
using V4MAutoMapper;

namespace ManagementRPG.Application.Global.Campanhas.Handlers
{
    public class CampanhaHandlerQuery : HandlerQuery<Campanha, CampanhaResponse, int, int>,
        ICommandHandler<CampanhaCommandGetAll, IEnumerable<CampanhaResponse>>,
        ICommandHandler<CampanhaCommandGetById, CampanhaResponse>,
        ICommandHandler<CampanhaCommandGetByJogador, IEnumerable<CampanhaJogadorDTO>>,
        ICommandHandler<CampanhaCommandGetByMestre, IEnumerable<CampanhaMestreDTO>>
    {
        private readonly ICampanhaRepository _repository;
        private readonly IMapper _mapper;

        public CampanhaHandlerQuery(ICampanhaRepository repository, IMapper mapper) : base(repository)
        {
            _repository = repository;
            _mapper = mapper;
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

        public async Task<Result<IEnumerable<CampanhaJogadorDTO>>> Handle(CampanhaCommandGetByJogador request, CancellationToken cancellationToken)
        {
            //Implementar o get das campanhas do jogador
            dynamic result = new { };

            return Result.SuccessChain(result, SuccessMethodTask<CampanhaCommandGetByJogador>.CommandMethod, SuccessTask.GetRunedMethodName());
        }

        public async Task<Result<IEnumerable<CampanhaMestreDTO>>> Handle(CampanhaCommandGetByMestre request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetByMestreId(request.mestreId);

            var dataDTO = data?.Select(_mapper.Map<CampanhaMestreDTO>) ?? Enumerable.Empty<CampanhaMestreDTO>();

            return Result.Success(dataDTO, SuccessMethodTask<CampanhaCommandGetByMestre>.CommandMethod, SuccessTask.GetRunedMethodName());
        }
    }
}
