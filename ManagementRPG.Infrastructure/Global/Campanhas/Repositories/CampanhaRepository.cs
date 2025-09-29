using ManagementRPG.Domain.Abstractions.Commands.Results;
using ManagementRPG.Domain.Abstractions.Context;
using ManagementRPG.Domain.Abstractions.Repositories;
using ManagementRPG.Domain.Shared.Commands;
using ManagementRPG.Domain.Global.Campanhas.Queries;
using ManagementRPG.Domain.Global.Campanhas.Repositories;
using ManagementRPG.Domain.Global.Campanhas.Entities;

namespace ManagementRPG.Infrastructure.Global.Campanhas.Repositories
{
    public class CampanhaRepository : Repository<Campanha, int, int, CampanhaQueryResult>, ICampanhaRepository
    {
        private static IList<Campanha> DataBase = Enumerable.Empty<Campanha>().ToList();

        public CampanhaRepository(IUnitOfWork uow)
            : base(uow, "100")
        {
        }

        public async Task<IEnumerable<CampanhaQueryResult>> GetAll()
        {
            if (DataBase == null)
                return default!;

            return DataBase.Select(c => new CampanhaQueryResult() 
            { 
                Id = c.Id,
                MestreId = c.MestreId,
                Nome = c.Nome,
                Descricao = c.Descricao,
                Sinopse = c.Sinopse,
                Status = c.Status,
                UserInsId = c.UserInsId,
                UserInsData = c.UserInsData,
                UserModId = c.UserModId,
                UserModData = c.UserModData
            });
        }

        public async Task<CampanhaQueryResult> GetById(int id)
        {
            if (DataBase == null)
                return default!;

            var entity = DataBase.Where(c => c.Id == id);

            if (!(entity.Count() > 0))
                return default!;
            
            return entity.Select(c => new CampanhaQueryResult()
            {
                Id = c.Id,
                MestreId = c.MestreId,
                Nome = c.Nome,
                Descricao = c.Descricao,
                Sinopse = c.Sinopse,
                Status = c.Status,
                UserInsId = c.UserInsId,
                UserInsData = c.UserInsData,
                UserModId = c.UserModId,
                UserModData = c.UserModData
            }).First();
        }

        public async Task<IEnumerable<CampanhaQueryResult>> GetByProperty<TProp>(TProp id)
        {
            return new List<CampanhaQueryResult>();
        }

        public async Task<int> Insert(Campanha entity)
        {
            if (DataBase != null)
                DataBase.Add(entity);

            AutoIncrementId();

            return DataBase!.LastOrDefault()!.Id;
        }

        public async Task<bool> Update(Campanha entity)
        {
            if (DataBase == null)
                return false;

            var idxEntity = DataBase.ToList().FindIndex(c => c.Id == entity.Id);
            DataBase.ToList()[idxEntity] = entity;
            return true;
        }

        public async Task<bool> Delete(Campanha entity)
        {
            if (DataBase == null)
                return true;

            var listB = DataBase.Select(c => new CampanhaQueryResult()
            {
                Id = c.Id,
                MestreId = c.MestreId,
                Nome = c.Nome,
                Descricao = c.Descricao,
                Sinopse = c.Sinopse,
                Status = c.Status,
                UserInsId = c.UserInsId,
                UserInsData = c.UserInsData,
                UserModId = c.UserModId,
                UserModData = c.UserModData
            });

            var values = DataBase.ToList().RemoveAll(c => c.Id == entity.Id);
            if (values == 1)
                return true;

            DataBase = listB.Select(command => new Campanha(command.Id, command.Status, command.UserInsId, command.UserInsData,
                command.UserModId, command.UserModData, command.MestreId, command.Nome, command.Descricao, command.Sinopse)).ToList();

            return true;
        }

        private bool AutoIncrementId()
        {
            var listB = DataBase.Select(c => new CampanhaQueryResult()
            {
                Id = c.Id,
                MestreId = c.MestreId,
                Nome = c.Nome,
                Descricao = c.Descricao,
                Sinopse = c.Sinopse,
                Status = c.Status,
                UserInsId = c.UserInsId,
                UserInsData = c.UserInsData,
                UserModId = c.UserModId,
                UserModData = c.UserModData
            });

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

                var newEntity = new Campanha(id, command.Status, command.UserInsId, command.UserInsData,
                    command.UserModId, command.UserModData, command.MestreId, command.Nome, command.Descricao, command.Sinopse);

                return newEntity;
             }).ToList();

            return true;
        }

        protected override object GetInsertObject(Campanha entity)
        {
            throw new NotImplementedException();
        }

        protected override object GetUpdateObject(Campanha entity)
        {
            throw new NotImplementedException();
        }
    }
}
