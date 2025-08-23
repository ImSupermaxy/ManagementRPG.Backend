using ManagementRPG.Domain.Queries.Campanhas;
using ManagementRPG.Domain.Abstractions.Commands.Results;
using ManagementRPG.Domain.Abstractions.Context;
using ManagementRPG.Domain.Abstractions.Repositories;
using ManagementRPG.Domain.Entities.Campanhas;
using ManagementRPG.Domain.Repositories.Campanhas;
using ManagementRPG.Domain.Shared.Commands;

namespace ManagementRPG.Infrastructure.Repositories.Campanhas
{
    public class CampanhaRepository : Repository<Campanha, int, int, CampanhaQueryResult>, ICampanhaRepository
    {
        private static IList<Campanha> DataBase = Enumerable.Empty<Campanha>().ToList();

        public CampanhaRepository(IUnitOfWork uow)
            : base(uow, "100")
        {
        }

        public async Task<IEnumerable<CampanhaQueryResult>> Get()
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

        public async Task<ICommandResult> Insert(Campanha entity)
        {
            if (DataBase != null)
                DataBase.Add(entity);

            return new CommandResult(true, "");
        }

        public async Task<ICommandResult> Update(Campanha entity)
        {
            if (DataBase == null)
                return new CommandResult(false, "Banco não iniciado");

            var idxEntity = DataBase.ToList().FindIndex(c => c.Id == entity.Id);
            DataBase.ToList()[idxEntity] = entity;
            return new CommandResult(true, "");
        }

        public async Task<ICommandResult> Delete(Campanha entity)
        {
            if (DataBase == null)
                return new CommandResult(false, "");

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
                return new CommandResult(true, "");

            DataBase = listB.Select(command => new Campanha(command.Id, command.Status, command.UserInsId, command.UserInsData,
                command.UserModId, command.UserModData, command.MestreId, command.Nome, command.Descricao, command.Sinopse)).ToList();

            return new CommandResult(false, $"{values} Rows affecteds");
        }
    }
}
