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

        protected abstract void Validate();

        public virtual void UpdateEntity()
        {
        }

        public void UpdateValid()
        {
            IsValid = !IsValid;

            if (IsValid)
                Errors.Clear();
        }

        public void UpdateValid(IList<ValidationFailure> errors)
        {
            IsValid = false;
            Errors = errors;
        }
    }

    public abstract class Entity<TId, TUId> : Entity<TId>
    {
        public TUId UserInsId { get; private set; }
        public DateTime UserInsData { get; private set; }
        public TUId UserModId { get; private set; }
        public DateTime UserModData { get; private set; }

        protected Entity(TUId userId)
            : base()
        {
            CreateUserLog(userId);
        }

        protected Entity(TId id, TUId userInsId, DateTime userInsData, TUId userModId, DateTime userModData)
            : base(id)
        {
            UserInsId = userInsId;
            UserInsData = userInsData;
            UserModId = userModId;
            UserModData = userModData;
        }

        protected Entity(TId id, TUId userInsId, DateTime userInsData, TUId userModId)
          : base(id)
        {
            UserInsId = userInsId;
            UserInsData = userInsData;
            UpdateUserMod(userModId);
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

        protected EntityDefault(TUId userId)
            : base(userId)
        {
            Status = EStatus.Ativo;
        }

        protected EntityDefault(TId id, EStatus status, TUId userInsId, DateTime userInsData, TUId userModId, DateTime userModData)
            : base(id, userInsId, userInsData, userModId, userModData)
        {
            Status = status;
        }

        protected EntityDefault(TId id, EStatus status, TUId userInsId, DateTime userInsData, TUId userModId)
          : base(id, userInsId, userInsData, userModId)
        {
            Status = status;
        }

        protected virtual void CreateUserLog(TUId userId)
        {
            Status = EStatus.Ativo;
            base.CreateUserLog(userId);
        }

        public void InactivateEntity()
        {
            Status = EStatus.Inativo;
        }
    }
}
