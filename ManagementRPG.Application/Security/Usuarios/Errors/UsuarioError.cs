using ManagementRPG.Domain.Abstractions.Errors;

namespace ManagementRPG.Application.Security.Usuarios.Errors
{

    public static class UsuarioError
    {
        public static readonly Error NotRegistered = new(
            "Notification.UsuarioNotRegistered",
            $"Usuario was not Registered, email or arroba was already in base");

        public static readonly Error FailureRegistered = new(
            "Notification.UsuarioFailureRegistered",
            $"Usuario register fail");

        public static readonly Error AlreadyExist = new(
            "Notification.UsuarioAlreadyExist",
            $"Usuario's email is already registered");

        public static readonly Error InvalidCredetials = new(
            "Notification.UsuarioInvalidCredetials",
            $"Usuario Email or(and) Password is(are) wrong");

        public static readonly Error FailureLogin = new(
            "Notification.UsuarioFailureLogin",
            $"Usuario Email or(and) Password is(are) wrong");

        public static readonly Error FailureAuthentication = new(
            "Notification.UsuarioFailureAuthentication",
            $"Usuario Email or(and) Password is(are) wrong");

        public static readonly Error NotAuthorized = new(
            "Notification.UsuarioNotAuthorized",
            $"This Usuario is not authorized");
    }
}
