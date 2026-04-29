using ManagementRPG.Domain.Security.Usuarios.Entities;
using ManagementRPG.Domain.Security.Usuarios.Responses;

namespace ManagementRPG.Domain.Security.Usuarios.Repositories
{
    public interface IUsuarioPerfilRepository
    {
        Task<IEnumerable<UsuarioPerfilResponse>> GetByUsuarioSistema(int usuarioId, int sistemaId);
        Task<bool> Insert(UsuarioPerfil[] perfis);
    }
}
