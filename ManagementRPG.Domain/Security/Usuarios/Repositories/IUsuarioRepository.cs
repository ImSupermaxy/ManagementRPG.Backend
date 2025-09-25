using ManagementRPG.Domain.Abstractions.Repositories;
using ManagementRPG.Domain.Security.Usuarios.Entities;
using ManagementRPG.Domain.Security.Usuarios.Enums;
using ManagementRPG.Domain.Security.Usuarios.Queries;

namespace ManagementRPG.Domain.Security.Usuarios.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario, int, int, UsuarioQueryResult>
    {
        Task<UsuarioQueryResult> GetByEmail(string email);
        Task<bool> UsuarioExist(string email = default!, string arroba = default!);
        Task<int> Register(Usuario entity);
        Task<bool> Authenticate(UsuarioAuthLog entity);
        Task<bool> InsertUpdatePerfis(EPerfil[] perfis, int usuarioId, int sistemaId);
    }
}
