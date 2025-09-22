using ManagementRPG.Domain.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementRPG.Domain.Abstractions.Errors
{
    public static class EntityError<T, TId>
        where T : Entity<TId>
    {
        public static readonly Error NotFound = new(
            "Notification.EntityNotFound",
            $"Entity {typeof(T).Name} Not Found.");

        public static readonly Error NotCreated = new(
            "Notification.EntityNotCreated",
            $"Entity {typeof(T).Name} was Not Created.");

        public static readonly Error NotUpdated = new(
            "Notification.EntityNotUpdated",
            $"Entity {typeof(T).Name} was Not Updated");

        public static readonly Error NotDeleted = new(
            "Notification.EntityNotDeleted",
            $"Entity {typeof(T).Name} was Not Deleted");

        public static readonly Error Invalid = new(
            "Notification.EntityNotValid",
            $"Entity {typeof(T).Name} is Not Valid");
    }
}
