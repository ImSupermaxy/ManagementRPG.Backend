using ManagementRPG.Domain.Utils;
using System.Runtime.CompilerServices;

namespace ManagementRPG.Domain.Abstractions.Messages.Successes
{
    public class SuccessTask : IMessage
    {
        public string Action { get; }
        public string Message { get; }
        public string Process { get; }

        public SuccessTask(string action, string message, string process = "")
        {
            Action = action;
            Message = message;
            Process = process;
        }

        public static readonly SuccessTask None = new(string.Empty, string.Empty);

        public static readonly SuccessTask NullValue = new("Success.NullValue", "Null value was provided");

        public static SuccessTask Create(string action, string message, [CallerMemberName] string process = "", [CallerFilePath] string filePath = "") => new SuccessTask(action, message, GetRunedMethodName(process, filePath));

        public static SuccessTask FromValidationSuccess(string action, string message, [CallerMemberName] string methodName = "")
        {
            return new SuccessTask(action, message, methodName);
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Process))
                return $"{Message} in action ({Action})\nSuccess in process: {Process}";

            return $"{Message} in action ({Action})";
        }
    
        public static string GetRunedMethodName([CallerMemberName] string methodName = "", [CallerFilePath] string filePath = "") => @$"Method: {methodName}; In path: {GetFilePathNormalized(filePath)}";

        private static string GetFilePathNormalized(string filePath) => Path.GetFullPath(filePath.Replace(UtilsGeneral.GetSolutionDirectory(), ""));
    }
}
