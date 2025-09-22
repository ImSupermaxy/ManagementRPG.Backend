using Dapper;
using ManagementRPG.Domain.Abstractions.Context;
using ManagementRPG.Domain.Abstractions.Repositories;
using ManagementRPG.Domain.Security.Usuarios.Entities;
using ManagementRPG.Domain.Security.Usuarios.Enums;
using ManagementRPG.Domain.Security.Usuarios.Queries;
using ManagementRPG.Domain.Security.Usuarios.Repositories;
using System.Data;

namespace ManagementRPG.Infrastructure.Security.Usuarios.Repositories
{
    public class UsuarioRepository : Repository<Usuario, int, int, UsuarioQueryResult>, IUsuarioRepository
    {
        public UsuarioRepository(IUnitOfWork uow) 
            : base(uow, "002")
        {
        }

        public async Task<UsuarioQueryResult> GetByEmail(string email)
        {
            var param = new DynamicParameters();
            param.Add("email", email);

            return await Uow.Context.Connection
                                .QueryFirstAsync<UsuarioQueryResult>($"{GetProcEntityName()}view ou proc",
                                    param,
                                    commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> Authenticate(UsuarioAuthLog entity)
        {
            var rows = await Uow.Context.Connection
                                .QueryFirstAsync<int>($"sp005insert",
                                    new
                                    {
                                        usuarioid = entity.UsuarioId,
                                        login = entity.Login,
                                        data = entity.Data,
                                        senha = entity.SenhaHash,
                                        token = entity.Token
                                    },
                                    transaction: Uow.Transaction,
                                    commandType: CommandType.StoredProcedure);
            return rows == 1;
        }

        public Task<bool> UsuarioExist(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Register(Usuario entity)
        {
            return await Insert(entity);
        }

        public Task<bool> InsertUpdatePerfis(EPerfil[] perfis, int usuarioId)
        {
            throw new NotImplementedException();
        }

        protected override object GetInsertObject(Usuario entity)
        {
            return new
            {
                nome = entity.Nome,
                email = entity.Email,
                arroba = entity.Arroba,
                senha = entity.Senha,
                userinsid = entity.UserInsId,
                userinsdate = entity.UserInsData,
                usermodid = entity.UserModId,
                usermoddate = entity.UserModData
            };
        }

        protected override object GetUpdateObject(Usuario entity)
        {
            return new
            {
                id = entity.Id,
                nome = entity.Nome,
                email = entity.Email,
                arroba = entity.Arroba,
                senha = entity.Senha,
                userinsid = entity.UserInsId,
                userinsdate = entity.UserInsData,
                usermodid = entity.UserModId,
                usermoddate = entity.UserModData
            };
        }
    }
}
