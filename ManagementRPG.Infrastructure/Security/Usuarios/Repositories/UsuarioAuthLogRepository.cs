using Dapper;
using ManagementRPG.Domain.Abstractions.Context;
using ManagementRPG.Domain.Security.Usuarios.Entities;
using ManagementRPG.Domain.Security.Usuarios.Repositories;
using ManagementRPG.Domain.Security.Usuarios.Responses;
using System.Data;
using System.Text;

namespace ManagementRPG.Infrastructure.Security.Usuarios.Repositories
{
    internal class UsuarioAuthLogRepository : IUsuarioAuthLogRepository
    {
        protected readonly IUnitOfWork Uow;
        protected readonly string _tableName;

        public UsuarioAuthLogRepository(IUnitOfWork uow)
        {
            Uow = uow;
            _tableName = "tbl_004_usuario_auth_log";
        }

        public async Task<IEnumerable<UsuarioAuthLogResponse>> GetAll(int skip, int take)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT ");
            sql.AppendLine("    t004.usuarioid, ");
            sql.AppendLine("    t004.login, ");
            sql.AppendLine("    t004.data, ");
            sql.AppendLine("    t004.senhahash, ");
            sql.AppendLine("    t004.token ");
            sql.AppendLine($"FROM {_tableName} t004 ");
            sql.AppendLine($"limit {take} offset {skip}; ");

            return await Uow.Context.Connection.QueryAsync<UsuarioAuthLogResponse>(
                sql.ToString(),
                commandType: CommandType.Text
            ) ?? Enumerable.Empty<UsuarioAuthLogResponse>();
        }

        public async Task<IEnumerable<UsuarioAuthLogResponse>> GetByUsuario(int usuarioId)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT ");
            sql.AppendLine("    t004.usuarioid, ");
            sql.AppendLine("    t004.login, ");
            sql.AppendLine("    t004.data, ");
            sql.AppendLine("    t004.senhahash, ");
            sql.AppendLine("    t004.token ");
            sql.AppendLine($"FROM {_tableName} t004 ");
            sql.AppendLine($"WHERE t004.usuarioid = {usuarioId}; ");

            return await Uow.Context.Connection.QueryAsync<UsuarioAuthLogResponse>(
                sql.ToString(),
                commandType: CommandType.Text
            ) ?? Enumerable.Empty<UsuarioAuthLogResponse>();
        }

        public async Task<bool> Authenticate(UsuarioAuthLog entity)
        {
            var sql = new StringBuilder();
            sql.AppendLine($"INSERT INTO {_tableName} (usuarioid, login, data, senhahash, token) ");
            sql.AppendLine("VALUES (@UsuarioId, @Login, @Data, @SenhaHash, @Token); ");

            await Uow.Context.Connection.ExecuteScalarAsync(
                sql.ToString(),
                entity,
                transaction: Uow.Transaction
            );

            return true;
        }
    }
}
