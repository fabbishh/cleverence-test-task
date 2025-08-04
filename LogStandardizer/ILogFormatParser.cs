using CleverenceTestTasks.LogStandardizer.Models;

namespace CleverenceTestTasks.LogStandardizer
{
	public interface ILogFormatParser
	{
		bool TryParse(string logLine, out LogEntry? entry);
	}
}
