using ManagementRPG.Domain.Abstractions.Commands.Inserts;
using ManagementRPG.Domain.Abstractions.Commands.Updates;
using ManagementRPG.Domain.Abstractions.Entities;
using ManagementRPG.Domain.Abstractions.Errors;
using ManagementRPG.Domain.Abstractions.Repositories;
using ManagementRPG.Domain.Abstractions.Responses;
using ManagementRPG.Domain.Shared.ApiConfig;
using ManagementRPG.Domain.Shared.Commands;
using V4MAutoMapper;

namespace ManagementRPG.Domain.Abstractions.Handlers
{
    public class HandlerEntity<T, TId, TInsert, TUpdate, TResponse> 
            : IHandlerEntity<TId, TInsert, TUpdate>
        where T : Entity<TId>
        where TInsert : ICommandInsert
        where TUpdate : ICommandUpdate<TId>
        where TResponse : IResponse<TId>
    {
        protected IRepository<T, TId, TResponse> Repository { get; set; }

        protected IMapper Mapper;

        public HandlerEntity(IRepository<T, TId, TResponse> repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public virtual async Task<Result<TId>> HandleInsert(TInsert command)
        {
            try
            {
                var entity = Mapper.Map<T>(command);

                if (!entity.IsValid)
                    return Result.Failure<TId>(EntityError<T, TId>.Invalid, entity.GetErrors());

                var newId = await Repository.Insert(entity);
                if (newId == IdentifierTypeManager<TId>.GetDefaultValue())
                    return Result.Failure<TId>(EntityError<T, TId>.NotCreated);

                return Result.Success(newId);
            }
            catch (Exception ex)
            {
                return Result.Failure<TId>(EntityError<T, TId>.NotCreated, [ex.Message]);
            }
        }

        public virtual async Task<Result> HandleUpdate(TUpdate command)
        {
            try
            {
                var query = await Repository.GetById(command.Id);
                if (query is null)
                    return Result.Failure(EntityError<T, TId>.Invalid);

                var entity = Mapper.Map<T>(command);

                if (!entity.IsValid)
                    return Result.Failure(EntityError<T, TId>.Invalid, entity.GetErrors());

                if (!(await Repository.Update(entity)))
                    return Result.Failure(EntityError<T, TId>.NotUpdated);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(EntityError<T, TId>.NotUpdated, [ex.Message]);
            }
        }

    }

    public class HandlerEntity<T, TId, TUId, TInsert, TUpdate, TResponse>
            : HandlerEntity<T, TId, TInsert, TUpdate, TResponse>,
            IHandlerEntity<TId, TUId, TInsert, TUpdate>
        where T : Entity<TId, TUId>
        where TInsert : ICommandInsert<TUId>
        where TUpdate : ICommandUpdate<TId, TUId>
        where TResponse : IResponse<TId, TUId>
    {

        protected HandlerEntity(IRepository<T, TId, TUId, TResponse> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

    }

    public class HandlerQuery<T, TResponse, TId> : IHandlerQuery<TResponse, TId>
        where T : Entity<TId>
        where TResponse : IResponse<TId>
    {
        protected IRepository<T, TId, TResponse> Repository { get; set; }

        public HandlerQuery(IRepository<T, TId, TResponse> repository)
        {
            Repository = repository;
        }

        public virtual async Task<Result<IEnumerable<TResponse>>> HandleGetAll()
        {
            try
            {
                var result = await Repository.GetAll();
                if (result == null)
                    return Result.Failure<IEnumerable<TResponse>>(EntityError<T, TId>.NotFound);

                return Result.Success(result);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<TResponse>>(EntityError<T, TId>.NotFound, [ex.Message]);
            }
        }

        public virtual async Task<Result<TResponse>> HandleGet(TId id)
        {
            try
            {
                var result = await Repository.GetById(id);
                if (result == null)
                    return Result.Failure<TResponse>(EntityError<T, TId>.NotFound);

                return Result.Success(result);
            }
            catch (Exception ex)
            {
                return Result.Failure<TResponse>(EntityError<T, TId>.NotFound);
            }
        }
    }

    public class HandlerQuery<T, TResponse, TId, TUId> : HandlerQuery<T, TResponse, TId>
        where T : Entity<TId, TUId>
        where TResponse : IResponse<TId, TUId>
    {
        public HandlerQuery(IRepository<T, TId, TUId, TResponse> repository) 
            : base(repository)
        { 
        }
    }
}
