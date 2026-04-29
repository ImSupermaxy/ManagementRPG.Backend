using Dapper;
using ManagementRPG.Domain.Abstractions.Context;
using ManagementRPG.Domain.Abstractions.Repositories;
using ManagementRPG.Domain.Security.System.Entities;
using ManagementRPG.Domain.Security.Usuarios.Entities;
using ManagementRPG.Domain.Security.Usuarios.Enums;
using ManagementRPG.Domain.Security.Usuarios.Repositories;
using ManagementRPG.Domain.Security.Usuarios.Responses;
using ManagementRPG.Domain.Shared.Commands;
using System.Data;
using System.Text;

namespace ManagementRPG.Infrastructure.Security.Usuarios.Repositories
{
    public class UsuarioRepository : Repository<Usuario, int, int, UsuarioResponse>, IUsuarioRepository
    {
        private static UsuarioResponse MasterUser = new UsuarioResponse() 
        {
            Id = 1,
            Nome = "Master",
            Arroba = "Master",
            Email = "master@teste.com",
            SenhaHash = "$2a$11$qIbYy.HFXGqNGRPDxJ.44ehhc0qmpeGjBwuEl2jljhLdK50TMf/Q2",
            Status = EStatusUsuario.Ativo,
            UserInsData = DateTime.Now,
            UserInsId = 1,
            UserModData = DateTime.Now,
            UserModId = 1,
        };
       
        public UsuarioRepository(IUnitOfWork uow) 
            : base(uow, "002")
        {
        }

        public async Task<UsuarioResponse> GetByEmail(string email)
        {
            //TODO:
            //Verificar o pq o GetByPRopertys Não funcionou...
            var sql = new StringBuilder();
            sql.AppendLine("SELECT ");
            sql.AppendLine("    * ");
            sql.AppendLine($"FROM tbl_002_usuario t002 ");
            sql.AppendLine($"WHERE t002.Email = '{email}'; ");

            return await Uow.Context.Connection.QueryFirstOrDefaultAsync<UsuarioResponse>(
                sql.ToString(),
                commandType: CommandType.Text
            ) ?? default!;
        }

        public async Task<bool> UsuarioExist(string email, string arroba)
        {
            return (await GetByPropertys(GetDefaultParams(email: email, arroba: arroba))) is not null;
        }

        public async Task<int> Register(Usuario entity)
        {
            return await Insert(entity);
        }

        protected override object GetInsertObject(Usuario entity)
        {
            return new
            {
                p_nome = entity.Nome,
                p_email = entity.Email,
                p_arroba = entity.Arroba,
                p_status = entity.Status,
                p_senhahash = entity.SenhaHash,
                p_userinsid = entity.UserInsId,
                p_userinsdata = entity.UserInsData,
            };
        }

        protected override object GetUpdateObject(Usuario entity)
        {
            return new
            {
                p_id = entity.Id,
                p_nome = entity.Nome,
                p_email = entity.Email,
                p_arroba = entity.Arroba,
                p_status = entity.Status,
                p_senhahash = entity.SenhaHash,
                p_usermodid = entity.UserModId,
                p_usermoddata = entity.UserModData
            };
        }

        private List<DataParam> GetDefaultParams(int? id = null, string email = null!, string arroba = null!, EStatusUsuario? status = null)
        {
            var props = new List<DataParam>()
            {
                new DataParam("id", id, DbType.Int32),
                new DataParam("nome", email, DbType.String),
                new DataParam("email", email, DbType.String),
                new DataParam("arroba", arroba, DbType.String),
                new DataParam("status", status, DbType.Int16),
            };

            return props;
        }
    }
}
