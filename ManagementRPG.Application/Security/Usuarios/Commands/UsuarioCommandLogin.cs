using ManagementRPG.Domain.Abstractions.Commands;

namespace ManagementRPG.Application.Security.Usuarios.Commands
{
    public record UsuarioCommandLogin(string Email, string Senha, string? CodeAuth) : ICommandResponse
    {
    }
}
