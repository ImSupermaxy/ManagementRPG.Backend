namespace ManagementRPG.Domain.Abstractions.Commands.Inserts
{
    public interface ICommandInsert : ICommandResponse
    {
    }

    public interface ICommandInsert<TUId> : ICommandInsert
    {
        public TUId UserId { get; set; }
    }
}
