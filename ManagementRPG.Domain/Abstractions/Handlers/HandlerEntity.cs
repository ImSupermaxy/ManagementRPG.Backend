using ManagementRPG.Domain.Abstractions.Commands.Inserts;
using ManagementRPG.Domain.Abstractions.Commands.Updates;
using ManagementRPG.Domain.Abstractions.Entities;
using ManagementRPG.Domain.Abstractions.Mappers;
using ManagementRPG.Domain.Abstractions.Queries.Results;
using ManagementRPG.Domain.Abstractions.Repositories;
using ManagementRPG.Domain.Shared.ApiConfig;
using ManagementRPG.Domain.Shared.Commands;
using System.Security.Cryptography;

namespace ManagementRPG.Domain.Abstractions.Handlers
{
    public class HandlerEntity<T, TId, TCommandInsert, TCommandUpdate, TCommandQuery> 
            : IHandlerEntity<TId, TCommandInsert, TCommandUpdate>
        where T : Entity<TId>
        where TCommandInsert : ICommandInsert
        where TCommandUpdate : ICommandUpdate<TId>
        where TCommandQuery : IQueryResult<TId>
    {
        protected IRepository<T, TId, TCommandQuery> Repository { get; set; }
        protected IMapperEntity<T, TId, TCommandInsert, TCommandUpdate, TCommandQuery> Mapper { get; set; }

        public HandlerEntity(IRepository<T, TId, TCommandQuery> repository, 
            IMapperEntity<T, TId, TCommandInsert, TCommandUpdate, TCommandQuery> mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public virtual async Task<CommandResult> HandleInsert(TCommandInsert command)
        {
            try
            {
                var entity = Mapper.GetEntity(command);

                if (!entity.IsValid)
                    return new CommandResult(false, "Entidade não é Valida!", entity.Errors);

                var newId = await Repository!.Insert(entity);
                if (newId != IdentifierTypeManager<TId>.GetDefaultValue())
                    return new CommandResult(false, $"A entidade {typeof(T).GetType().Name} não foi inserida!", entity.Errors);

                return new CommandResult(true, "", newId);
            }
            catch (Exception ex)
            {
                var message = $"Erro ao inserir a entidade {typeof(T).GetType().Name}" + (!RunMode.IsProd() ? $": {ex.Message}" : "");
                return new CommandResult(false, message);
            }
        }

        public virtual async Task<CommandResult> HandleUpdate(TCommandUpdate command)
        {
            try
            {
                var query = await Repository.GetById(command.Id);
                var entity = Mapper.GetEntity(query, command);

                if (!entity.IsValid)
                    return new CommandResult(false, "Entidade não é Valida!", entity.Errors);

                if (!(await Repository.Update(entity)))
                    return new CommandResult(false, $"A entidade {typeof(T).GetType().Name} não foi inserida!", entity.Errors);

                return new CommandResult(true, $"A entidade {typeof(T).GetType().Name} atualizada com sucesso!");
            }
            catch (Exception ex)
            {
                var message = $"Erro ao atualizar a entidade {typeof(T).GetType().Name}\n" + (!RunMode.IsProd() ? $": {ex.Message}" : "");
                return new CommandResult(false, message);
            }
        }

    }

    public class HandlerEntity<T, TId, TUId, TCommandInsert, TCommandUpdate, TCommandQuery>
            : HandlerEntity<T, TId, TCommandInsert, TCommandUpdate, TCommandQuery>,
            IHandlerEntity<TId, TUId, TCommandInsert, TCommandUpdate>
        where T : Entity<TId, TUId>
        where TCommandInsert : ICommandInsert<TUId>
        where TCommandUpdate : ICommandUpdate<TId, TUId>
        where TCommandQuery : IQueryResult<TId, TUId>
    {

        protected HandlerEntity(IRepository<T, TId, TUId, TCommandQuery> repository, 
            IMapperEntity<T, TId, TUId, TCommandInsert, TCommandUpdate, TCommandQuery> mapper) 
            : base(repository, mapper)
        {

        }

    }

    public class HandlerQuery<T, TCommandQuery, TId> : IHandlerQuery<TCommandQuery, TId>
        where T : Entity<TId>
        where TCommandQuery : IQueryResult<TId>
    {
        protected IRepository<T, TId, TCommandQuery> Repository { get; set; }

        public HandlerQuery(IRepository<T, TId, TCommandQuery> repository)
        {
            Repository = repository;
        }

        public virtual async Task<CommandResult<IEnumerable<TCommandQuery>>> HandleGetAll()
        {
            try
            {
                var result = await Repository.Get();
                if (result == null)
                    return new CommandResult<IEnumerable<TCommandQuery>>(true, "Nenhum registro encontrado",
                        Enumerable.Empty<TCommandQuery>());

                return new CommandResult<IEnumerable<TCommandQuery>>(true, "", result);
            }
            catch (Exception ex)
            {
                var message = $"Erro ao obter as entidades {typeof(T).GetType().Name}" + (!RunMode.IsProd() ? $": {ex.Message}" : "");
                return new CommandResult<IEnumerable<TCommandQuery>>(false, message, Enumerable.Empty<TCommandQuery>());
            }
        }

        public virtual async Task<CommandResult<TCommandQuery>> HandleGet(TId id)
        {
            try
            {
                var result = await Repository.GetById(id);
                if (result == null)
                    return new CommandResult<TCommandQuery>(true, "Registro não encontrado");

                return new CommandResult<TCommandQuery>(true, "", result);
            }
            catch (Exception ex)
            {
                var message = $"Erro ao obter a entidade {typeof(T).GetType().Name}" + (!RunMode.IsProd() ? $": {ex.Message}" : "");
                return new CommandResult<TCommandQuery>(false, message, default!);
            }
        }
    }

    public class HandlerQuery<T, TCommandQuery, TId, TUId> : HandlerQuery<T, TCommandQuery, TId>
        where T : Entity<TId, TUId>
        where TCommandQuery : IQueryResult<TId, TUId>
    {
        public HandlerQuery(IRepository<T, TId, TUId, TCommandQuery> repository) 
            : base(repository)
        { 
        }
    }
}
