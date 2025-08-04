using CleverenceTestTasks.LogStandardizer.Models;
using CleverenceTestTasks.LogStandardizer.Parsers;

namespace CleverenceTestTasks.LogStandardizer
{
	public class LogParser
	{
		private readonly List<ILogFormatParser> _parsers = new()
		{
			new Format1Parser(),
			new Format2Parser()
		};

		public bool TryParse(string logLine, out LogEntry? entry)
		{
			foreach (var parser in _parsers)
			{
				if (parser.TryParse(logLine, out entry))
					return true;
			}
			entry = null;

			return false;
		}
	}
}
