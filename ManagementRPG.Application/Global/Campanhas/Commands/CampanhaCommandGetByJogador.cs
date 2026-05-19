using ManagementRPG.Application.Global.Campanhas.DTOs;
using ManagementRPG.Domain.Abstractions.Commands;

namespace ManagementRPG.Application.Global.Campanhas.Commands
{
    public sealed record CampanhaCommandGetByJogador(int jogadorId) : ICommandResponse<IEnumerable<CampanhaJogadorDTO>>
    {
    }
}
