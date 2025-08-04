using CleverenceTestTasks.LogStandardizer.Models;
using System;

namespace CleverenceTestTasks.LogStandardizer.Utils
{
    public static class LogLevelMapper
    {
        public static LogLevel Map(string level)
        {
            return level switch
            {
                "INFORMATION" or "INFO" => LogLevel.INFO,
                "WARNING" or "WARN" => LogLevel.WARN,
                "ERROR" => LogLevel.ERROR,
                "DEBUG" => LogLevel.DEBUG,
                _ => throw new ArgumentException($"Unknown log level: {level}")
            };
        }
    }
}