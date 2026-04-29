using ManagementRPG.Domain.Security.Usuarios.Entities;
using ManagementRPG.Domain.Security.Usuarios.Responses;

namespace ManagementRPG.Domain.Security.Usuarios.Repositories
{
    public interface IUsuarioAuthLogRepository
    {
        Task<IEnumerable<UsuarioAuthLogResponse>> GetAll(int skip, int take);
        Task<IEnumerable<UsuarioAuthLogResponse>> GetByUsuario(int usuarioId);
        Task<bool> Authenticate(UsuarioAuthLog entity);
    }
}
