using Dapper;
using ManagementRPG.Domain.Abstractions.Context;
using ManagementRPG.Domain.Abstractions.Repositories;
using ManagementRPG.Domain.Security.Usuarios.Entities;
using ManagementRPG.Domain.Security.Usuarios.Enums;
using ManagementRPG.Domain.Security.Usuarios.Queries;
using ManagementRPG.Domain.Security.Usuarios.Repositories;
using System.Data;
using static Dapper.SqlMapper;

namespace ManagementRPG.Infrastructure.Security.Usuarios.Repositories
{
    public class UsuarioRepository : Repository<Usuario, int, int, UsuarioQueryResult>, IUsuarioRepository
    {
        private static UsuarioQueryResult MasterUser = new UsuarioQueryResult() 
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
        private static IList<Usuario> DataBase = new List<Usuario>() { GetByQueryResultStatic(MasterUser) };
        private static IList<UsuarioAuthLog> DataBaseLog = Enumerable.Empty<UsuarioAuthLog>().ToList();
        private static IList<TmpSistemaUsuarioPerfil> DataBasePerfil = Enumerable.Empty<TmpSistemaUsuarioPerfil>().ToList();
        private static IList<Usuario> DataBaseToken = Enumerable.Empty<Usuario>().ToList();

        public UsuarioRepository(IUnitOfWork uow) 
            : base(uow, "002")
        {
        }
        public async Task<IEnumerable<UsuarioQueryResult>> Get()
        {
            if (DataBase == null)
                return default!;

            return DataBase.Select(GetByEntity);
        }

        public async Task<UsuarioQueryResult> GetByEmail(string email)
        {
            if (DataBase == null)
                return default!;

            var entity = DataBase.Where(c => c.Email == email);

            if (!(entity.Count() > 0))
                return default!;

            return entity.Select(GetByEntity).First();

            var param = new DynamicParameters();
            param.Add("email", email);

            return await Uow.Context.Connection
                                .QueryFirstAsync<UsuarioQueryResult>($"{GetProcEntityName()}view ou proc",
                                    param,
                                    commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> Authenticate(UsuarioAuthLog entity)
        {
            if (DataBaseLog != null)
                DataBaseLog.Add(entity);

            return true;

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
            if (DataBase == null)
                return false;

            if (email == default && arroba == default)
                return false;

            if (!string.IsNullOrEmpty(email))
            {
                var user2 = DataBase.Where(u => u.Email == email).FirstOrDefault();
                return user2 != null;
            }

            var user = DataBase.Where(u => u.Arroba == arroba).FirstOrDefault();
            return user != null;
        }

        public async Task<int> Register(Usuario entity)
        {
            if (DataBase != null)
                DataBase.Add(entity);

            AutoIncrementId();

            return DataBase!.LastOrDefault()!.Id;

            return await Insert(entity);
        }

        public async Task<bool> Update(Usuario entity)
        {
            if (DataBase == null)
                return false;

            var idxEntity = DataBase.ToList().FindIndex(c => c.Id == entity.Id);
            DataBase.ToList()[idxEntity] = entity;
            return true;
        }

        public async Task<bool> InsertUpdatePerfis(EPerfil[] perfis, int usuarioId, int sistemaId)
        {
            //Deleção dos perfís antigos
            if (DataBasePerfil == null)
                return true;

            var listB = DataBasePerfil.Select(GetByEntity);

            var values = DataBasePerfil.ToList().RemoveAll(c => c.UsuarioId == usuarioId && c.SistemaId == sistemaId);
            if (!(values > 1))
            {
                DataBasePerfil = listB.Select(GetByEntity).ToList();

                return true;
            }

            //Insert dos novos perfis
            foreach (var perfil in perfis)
                DataBasePerfil.Add(new TmpSistemaUsuarioPerfil() { 
                    UsuarioId = usuarioId, 
                    SistemaId = sistemaId, 
                    Perfil = perfil 
                });

            return true;

            var rows = 0;
            foreach (var perfil in perfis)
                rows += await Uow.Context.Connection
                                .QueryFirstAsync<int>($"sp003insert",
                                    new
                                    {
                                        usuarioId,
                                        sistemaId,
                                        perfil
                                    },
                                    transaction: Uow.Transaction,
                                    commandType: CommandType.StoredProcedure);
            return rows == perfis.Length;
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


        private bool AutoIncrementId()
        {
            var listB = DataBase.Select(GetByEntity);

            DataBase = listB.OrderBy(d => d.UserInsData).Select(command => {
                var id = 1;
                if (command.Id != 0)
                    id = command.Id;
                else
                {
                    var lastWithId = DataBase.OrderBy(d => d.UserInsData).ToList().ElementAtOrDefault(DataBase.Count() - 2);
                    if (lastWithId != null)
                        id = lastWithId.Id + 1;
                }

                command.Id = id;
                var newEntity = GetByQueryResult(command);

                return newEntity;
            }).ToList();

            return true;
        }

        private UsuarioQueryResult GetByEntity(Usuario c)
        {
            return new UsuarioQueryResult()
            {
                Id = c.Id,
                Nome = c.Nome,
                Email = c.Email,
                Arroba = c.Arroba,
                SenhaHash = c.Senha,
                Status = c.Status,
                UserInsId = c.UserInsId,
                UserInsData = c.UserInsData,
                UserModId = c.UserModId,
                UserModData = c.UserModData
            };
        }

        private TmpSistemaUsuarioPerfil GetByEntity(TmpSistemaUsuarioPerfil c)
        {
            return new TmpSistemaUsuarioPerfil()
            {
                UsuarioId = c.UsuarioId,
                SistemaId = c.SistemaId,
                Perfil = c.Perfil
            };
        }

        private UsuarioAuthLogQueryResult GetByEntity(UsuarioAuthLog c)
        {
            return new UsuarioAuthLogQueryResult()
            {
                UsuarioId = c.UsuarioId,
                Login = c.Login,
                Data = c.Data,
                SenhaHash = c.SenhaHash,
                Token = c.Token
            };
        }

        private Usuario GetByQueryResult(UsuarioQueryResult query)
        {
            var newEntity = new Usuario(query.Id, query.Status, query.UserInsId, query.UserInsData,
                    query.UserModId, query.UserModData, query.Nome, query.Email, query.Arroba);
            newEntity.UpdateSenha(query.SenhaHash);
            return newEntity;
        }

        private static Usuario GetByQueryResultStatic(UsuarioQueryResult query)
        {
            var newEntity = new Usuario(query.Id, query.Status, query.UserInsId, query.UserInsData,
                    query.UserModId, query.UserModData, query.Nome, query.Email, query.Arroba);
            newEntity.UpdateSenha(query.SenhaHash);
            return newEntity;
        }

        private UsuarioAuthLog GetByQueryResult(UsuarioAuthLogQueryResult query)
        {
            var newEntity = new UsuarioAuthLog(query.UsuarioId, query.Login, query.SenhaHash, query.Token);
            return newEntity;
        }
    }

    public class TmpSistemaUsuarioPerfil
    {
        public int SistemaId { get; set; }
        public int UsuarioId { get; set; }
        public required EPerfil Perfil { get; set; }
    }
}
