using System.Data;

namespace ManagementRPG.Domain.Shared.Commands
{
    public class DataParam
    {
        public string ParamName { get; }
        public object ParamValue { get; }
        public DbType TypeValue { get; }

        public DataParam(string name, object value, DbType type)
        {
            ParamName = name;
            ParamValue = value;
            TypeValue = type;
        }
    }
}
