using Dapper;
using ManagementRPG.Domain.Abstractions.Context;
using ManagementRPG.Domain.Abstractions.Repositories;
using ManagementRPG.Domain.Security.Usuarios.Entities;
using ManagementRPG.Domain.Security.Usuarios.Enums;
using ManagementRPG.Domain.Security.Usuarios.Responses;
using ManagementRPG.Domain.Security.Usuarios.Repositories;
using ManagementRPG.Domain.Shared.Commands;
using System.Data;

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
            var param = new DynamicParameters();
            param.Add("email", email);

            return await Uow.Context.Connection
                                .QueryFirstAsync<UsuarioResponse>($"{GetProcEntityName()}view ou proc",
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

        public async Task<bool> UsuarioExist(string email, string arroba)
        {
            var props = new List<DataParam>()
            {
                new DataParam(email, "email"),
                new DataParam(arroba, "arroba"),
            };

            return (await GetByPropertys(props)) is not null;
        }

        public async Task<int> Register(Usuario entity)
        {
            return await Insert(entity);
        }

        public async Task<bool> InsertUpdatePerfis(EPerfil[] perfis, int usuarioId, int sistemaId)
        {
            var sql = "";

            var result = await Uow.Context.Connection
                .QueryFirstAsync<int>(sql.ToString(),
                    perfis.Select(perfil =>
                    new
                    {
                        usuarioId,
                        sistemaId,
                        perfil
                    }),
                    transaction: Uow.Transaction,
                    commandType: CommandType.Text);

            throw new NotImplementedException("NAO FOI TERMINADO!!");
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
