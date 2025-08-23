namespace ManagementRPG.Domain.Abstractions.Commands.Updates
{
    public interface ICommandUpdate<TId> : ICommandResponse
    {
        public TId Id { get; set; }
    }

    public interface ICommandUpdate<TId, TUId> : ICommandUpdate<TId>
    {
        public TUId UserId { get; set; }
    }
}
