using FluentValidation.Results;
using ManagementRPG.Domain.Shared.Enums;

namespace ManagementRPG.Domain.Abstractions.Entities
{
    public abstract class Entity<TId> : IEntity
    {
        public TId Id { get; private init; } = default!;
        public bool IsValid { get; private set; } = false;
        public IList<ValidationFailure> Errors { get; private set; } = Enumerable.Empty<ValidationFailure>().ToList();

        protected Entity() { }

        protected Entity(TId id)
        {
            Id = id;
        }

        public virtual void UpdateEntity() { }

        public void UpdateValid()
        {
            IsValid = !IsValid;

            if (IsValid)
                Errors.Clear();
        }

        public void UpdateValid(bool isValid, IList<ValidationFailure> errors)
        {
            IsValid = isValid;
            Errors = errors;

            if (errors == null)
                Errors = Enumerable.Empty<ValidationFailure>().ToList();
        }

        public string[] GetErrors()
        {
            return Errors.Select(e => $"Property: {e.PropertyName};\nMessage: {e.ErrorMessage}").ToArray();
        }
    }

    public abstract class Entity<TId, TUId> : Entity<TId>
    {
        public TUId UserInsId { get; private set; }
        public TUId UserModId { get; private set; }
        public DateTime UserInsData { get; private set; }
        public DateTime UserModData { get; private set; }

        //Insert
        protected Entity(TUId userId)
            : base()
        {
            CreateUserLog(userId);
        }

        //Update
        protected Entity(TId id, TUId userId)
            : base(id)
        {
            CreateUserLog(userId);
        }

        protected virtual void CreateUserLog(TUId userId)
        {
            var now = DateTime.Now;
            UserInsId = userId;
            UserInsData = now;
            UserModId = userId;
            UserModData = now;
        }

        protected void UpdateUserMod(TUId userId)
        {
            UserModId = userId;
            UserModData = DateTime.Now;
        }
    }

    public abstract class EntityDefault<TId, TUId> : Entity<TId, TUId>
    {
        public EStatus Status { get; private set; }

        //Insert
        protected EntityDefault(TUId userId)
            : base(userId)
        {
            Status = EStatus.Ativo;
        }

        //Update
        protected EntityDefault(TId id, TUId userId, EStatus status)
            : base(id, userId)
        {
            Status = status;
        }

        public void InactivateEntity()
        {
            Status = EStatus.Inativo;
        }
    }
}
