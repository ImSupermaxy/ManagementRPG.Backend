namespace ManagementRPG.Domain.Shared.ApiConfig
{
    public static class IdentifierTypeManager<TId>
    {
        private static int intValue = 1;
        private static Guid guidValue = default; //value default para o guid
        private static Type[] validTypes = [guidValue.GetType(), intValue.GetType()];

        public static bool IsValidTypeIdentifier()
        {
            var tipo = typeof(TId);

            return validTypes.Contains(tipo);
        }

        public static dynamic GetSystemDefaultValue()
        {
            if (!IsValidTypeIdentifier())
                return default!;

            var tipo = typeof(TId);

            if (tipo == intValue.GetType())
                return intValue;

            if (tipo == guidValue.GetType())
                return guidValue;

            return default!;
        }

        public static dynamic GetDefaultValue()
        {
            if (!IsValidTypeIdentifier())
                return default!;

            var tipo = typeof(TId);

            if (tipo == intValue.GetType())
                return (int)default;

            if (tipo == guidValue.GetType())
                return (Guid)default;

            return default!;
        }

        public static TId ParseStringToTypeId(string id)
        {
            if (!IsValidTypeIdentifier())
                return default!;

            var tipo = typeof(TId);

            if (tipo == intValue.GetType())
                return (TId)(int.Parse(id) as object);

            if (tipo == guidValue.GetType())
                return (TId)(new Guid(id) as object);

            return default!;
        }
    }
}
