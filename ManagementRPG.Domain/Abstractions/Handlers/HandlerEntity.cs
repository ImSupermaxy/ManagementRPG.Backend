using ManagementRPG.Domain.Abstractions.Commands.Inserts;
using ManagementRPG.Domain.Abstractions.Commands.Updates;
using ManagementRPG.Domain.Abstractions.Entities;
using ManagementRPG.Domain.Abstractions.Errors;
using ManagementRPG.Domain.Abstractions.Mappers;
using ManagementRPG.Domain.Abstractions.Queries.Results;
using ManagementRPG.Domain.Abstractions.Repositories;
using ManagementRPG.Domain.Shared.ApiConfig;
using ManagementRPG.Domain.Shared.Commands;

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

        public virtual async Task<Result<TId>> HandleInsert(TCommandInsert command)
        {
            try
            {
                var entity = Mapper.GetEntity(command);

                if (!entity.IsValid)
                    return Result.Failure<TId>(EntityError<T, TId>.Invalid); //entity.Errors???

                var newId = await Repository.Insert(entity);
                if (newId == IdentifierTypeManager<TId>.GetDefaultValue())
                    return Result.Failure<TId>(EntityError<T, TId>.NotCreated); //entity.Errors???

                return Result.Success(newId);
            }
            catch (Exception ex)
            {
                return Result.Failure<TId>(EntityError<T, TId>.NotCreated, ex.Message);
            }
        }

        public virtual async Task<Result> HandleUpdate(TCommandUpdate command)
        {
            try
            {
                var query = await Repository.GetById(command.Id);
                var entity = Mapper.GetEntity(query, command);

                if (!entity.IsValid)
                    return Result.Failure(EntityError<T, TId>.Invalid); //entity.Errors???

                if (!(await Repository.Update(entity)))
                    return Result.Failure(EntityError<T, TId>.NotUpdated); //entity.Errors???

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(EntityError<T, TId>.NotUpdated, ex.Message);
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

        public virtual async Task<Result<IEnumerable<TCommandQuery>>> HandleGetAll()
        {
            try
            {
                var result = await Repository.GetAll();
                if (result == null)
                    return Result.Failure<IEnumerable<TCommandQuery>>(EntityError<T, TId>.NotFound);

                return Result.Success(result);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<TCommandQuery>>(EntityError<T, TId>.NotFound, ex.Message);
            }
        }

        public virtual async Task<Result<TCommandQuery>> HandleGet(TId id)
        {
            try
            {
                var result = await Repository.GetById(id);
                if (result == null)
                    return Result.Failure<TCommandQuery>(EntityError<T, TId>.NotFound);

                return Result.Success(result);
            }
            catch (Exception ex)
            {
                return Result.Failure<TCommandQuery>(EntityError<T, TId>.NotFound);
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
