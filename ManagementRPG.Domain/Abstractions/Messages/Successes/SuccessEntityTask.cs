using ManagementRPG.Domain.Abstractions.Commands;
using ManagementRPG.Domain.Abstractions.Entities;

namespace ManagementRPG.Domain.Abstractions.Messages.Successes
{
    public class SuccessEntityTask<T, TId>
        where T : Entity<TId>
    {
        public static readonly SuccessTask Get = new(
            "GET",
            $"Entity(s) {typeof(T).Name} was Founded.");

        public static readonly SuccessTask Post = new(
            "POST",
            $"Entity(s) {typeof(T).Name} was Created.");

        public static readonly SuccessTask Put = new(
            "PUT",
            $"Entity(s) {typeof(T).Name} was Updated");

        public static readonly SuccessTask Patch = new(
            "PATCH",
            $"Entity(s) {typeof(T).Name} was Updated");

        public static readonly SuccessTask Delete = new(
            "DELETE",
            $"Entity(s) {typeof(T).Name} was Deleted");
    }

    public class SuccessMethodTask
    {
        public static SuccessTask CustomMethod(string nameMethod, string process) => new(
            nameMethod,
            $"Action in method was completed successfuly", 
            process);
    }

    public class SuccessMethodTask<TCommand>
         where TCommand : ICommand
    {
        public static SuccessTask CommandMethod => new(
            $"{typeof(TCommand).Name}",
            $"Action in method was completed successfuly");
    }
}
