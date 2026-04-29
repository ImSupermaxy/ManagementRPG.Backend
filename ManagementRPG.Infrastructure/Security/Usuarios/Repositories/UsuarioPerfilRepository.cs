using Dapper;
using ManagementRPG.Domain.Abstractions.Context;
using ManagementRPG.Domain.Abstractions.Entities;
using ManagementRPG.Domain.Security.Usuarios.Entities;
using ManagementRPG.Domain.Security.Usuarios.Enums;
using ManagementRPG.Domain.Security.Usuarios.Repositories;
using ManagementRPG.Domain.Security.Usuarios.Responses;
using System.Data;
using System.Security.AccessControl;
using System.Text;

namespace ManagementRPG.Infrastructure.Security.Usuarios.Repositories
{
    public class UsuarioPerfilRepository : IUsuarioPerfilRepository
    {
        protected readonly IUnitOfWork Uow;
        protected readonly string _tableName;

        public UsuarioPerfilRepository(IUnitOfWork uow)
        {
            Uow = uow;
            _tableName = "tbl_003_usuario_perfil";
        }

        public async Task<IEnumerable<UsuarioPerfilResponse>> GetByUsuarioSistema(int usuarioId, int sistemaId)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT ");
            sql.AppendLine("    t003.usuarioid, ");
            sql.AppendLine("    t003.sistemaid, ");
            sql.AppendLine("    t003.perfil ");
            sql.AppendLine($"FROM {_tableName} t003 ");
            sql.AppendLine($"WHERE t003.usuarioid = {usuarioId} ");
            sql.AppendLine($"AND t003.sistemaid = {sistemaId};");

            return await Uow.Context.Connection.QueryAsync<UsuarioPerfilResponse>(
                sql.ToString(),
                commandType: CommandType.Text
            ) ?? Enumerable.Empty<UsuarioPerfilResponse>();
        }

        public async Task<bool> Insert(UsuarioPerfil[] perfis)
        {
            var sql = new StringBuilder();
            sql.AppendLine("DECLARE novo_id INT; ");

            sql.AppendLine($"INSERT INTO {_tableName} (sistemaid, usuarioid, perfil) ");
            sql.AppendLine("VALUES (@SistemaId, @UsuarioId, @Perfil) ");

            sql.AppendLine("RETURNING id INTO novo_id; ");
            sql.AppendLine("RETURN novo_id; ");

            var result = await Uow.Context.Connection.ExecuteScalarAsync<int>(
                sql.ToString(),
                perfis,
                transaction: Uow.Transaction
            );

            return result == perfis.Length;
        }
    }
}
