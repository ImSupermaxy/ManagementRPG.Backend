using FluentValidation;
using ManagementRPG.Domain.Abstractions.Entities;
using ManagementRPG.Domain.Shared.Enums;

namespace ManagementRPG.Domain.Abstractions.Validators
{
    public abstract class ValidatorEntity<T, TId> : AbstractValidator<T>, IValidator where T : Entity<TId>
    {
        protected T Entity { get; set; }

        public ValidatorEntity(T entity)
        {
            RuleFor(e => e.Id).NotNull()/*.NotEqual((TId)default!)*/.WithMessage($"{typeof(T).Name} Id é obrigatório(a)");

            Entity = entity;
        }

        public void ValidateEntity() {
            var result = Validate(Entity);
            Entity.UpdateValid(result.IsValid, result.Errors);
        }
    }

    public abstract class ValidatorEntity<T, TId, TUId> : ValidatorEntity<T, TId> where T : Entity<TId, TUId>
    {
        public ValidatorEntity(T entity)
            : base(entity)
        {
            RuleFor(e => e.UserInsId).NotNull().NotEqual((TUId)default!).WithMessage($"{typeof(T).Name} inserção usuário deve possuir um valor válido");
            RuleFor(e => e.UserInsData).NotNull()
                .NotEqual(DateTime.MinValue)
                .NotEqual(DateTime.MaxValue)
                .InclusiveBetween(DateTime.Now.AddHours(-48), DateTime.Now.AddHours(24))
                .WithMessage($"{typeof(T).Name} data inserção deve possuir um valor válido");

            RuleFor(e => e.UserModId).NotNull().NotEqual((TUId)default!).WithMessage($"{typeof(T).Name} atualização usuário deve possuir um valor válido");
            RuleFor(e => e.UserInsData).NotNull()
                .NotEqual(DateTime.MinValue)
                .NotEqual(DateTime.MaxValue)
                .InclusiveBetween(DateTime.Now.AddHours(-48), DateTime.Now.AddHours(24))
                .WithMessage($"{typeof(T).Name} data atualização deve possuir um valor válido");
        }
    }

    public abstract class ValidatorEntityDefault<T, TId, TUId> : ValidatorEntity<T, TId> where T : EntityDefault<TId, TUId>
    {
        public ValidatorEntityDefault(T entity)
           : base(entity)
        {
            RuleFor(e => e.Status).NotNull().NotEqual(EStatus.Todos).WithMessage($"{typeof(T).Name} Status deve possuir um valor válido");
        }
    }
}
