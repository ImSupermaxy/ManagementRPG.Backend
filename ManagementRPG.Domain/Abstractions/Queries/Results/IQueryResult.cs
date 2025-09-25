using ManagementRPG.Domain.Shared.Enums;

namespace ManagementRPG.Domain.Abstractions.Queries.Results
{
    public interface IQueryResult<TId> : IQuery
    {
        public TId Id { get; set; }
    }

    public interface IQueryResult<TId, TUId> : IQueryResult<TId>
    {
        public int UserInsId { get; set; }
        public DateTime UserInsData { get; set; }
        public int UserModId { get; set; }
        public DateTime UserModData { get; set; }
    }

    public interface IQueryDefaultResult<TId, TUId> : IQueryResult<TId, TUId>
    {
        public EStatus Status { get; set; }
    }
}
