using ManagementRPG.Domain.Shared.Enums;

namespace ManagementRPG.Domain.Abstractions.Responses
{
    public interface IResponse<TId>
    {
        public TId Id { get; set; }
    }

    public interface IResponse<TId, TUId> : IResponse<TId>
    {
        public int UserInsId { get; set; }
        public DateTime UserInsData { get; set; }
        public int UserModId { get; set; }
        public DateTime UserModData { get; set; }
    }

    public interface IResponseDefault<TId, TUId> : IResponse<TId, TUId>
    {
        public EStatus Status { get; set; }
    }
}
