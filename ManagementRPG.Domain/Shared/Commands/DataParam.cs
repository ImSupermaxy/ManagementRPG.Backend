namespace ManagementRPG.Domain.Shared.Commands
{
    public class DataParam
    {
        public string ParamName { get; }
        public object ParamValue { get; }

        public DataParam(string name, object value)
        {
            ParamName = name;
            ParamValue = value;
        }
    }
}
