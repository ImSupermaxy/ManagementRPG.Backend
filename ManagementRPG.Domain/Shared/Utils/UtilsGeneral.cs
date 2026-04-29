namespace ManagementRPG.Domain.Utils
{
    public static class UtilsGeneral
    {
        public static string GetSolutionDirectory()
        {
            var directory = Directory.GetCurrentDirectory();
            var backendPath = Path.GetDirectoryName(directory);
            var projectPath = Path.GetDirectoryName(backendPath);

            return projectPath ?? "";
        }
    }
}
