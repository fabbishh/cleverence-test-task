namespace CleverenceTestTasks
{
    public static class AppConfig
    {
        private static readonly string ProjectRoot =
            Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\.."));

        public static string LogInputFilePath { get; } = Path.Combine(ProjectRoot, "logs.txt");
        public static string LogOutputFilePath { get; } = Path.Combine(ProjectRoot, "logs_standardized.txt");
        public static string LogProblemsFilePath { get; } = Path.Combine(ProjectRoot, "problems.txt");
    }
}