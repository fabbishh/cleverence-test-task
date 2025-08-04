using CleverenceTestTasks.LogStandardizer.Models;
using CleverenceTestTasks.LogStandardizer.Utils;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CleverenceTestTasks.LogStandardizer.Parsers
{
	public class Format2Parser : ILogFormatParser
	{
		private static readonly Regex Regex = new(
			@"^(\d{4}-\d{2}-\d{2})\s+(\d{2}:\d{2}:\d{2}\.\d+)\|\s*(INFO|WARN|ERROR|DEBUG)\|\d+\|([^|]+)\|\s*(.+)$",
			RegexOptions.Compiled);

		public bool TryParse(string logLine, out LogEntry? entry)
		{
			entry = null;
			if (string.IsNullOrEmpty(logLine))
				return false;

			var match = Regex.Match(logLine);
			if (!match.Success) 
				return false;

			var date = DateTime.ParseExact(match.Groups[1].Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
			var time = match.Groups[2].Value;
			var logLevel = LogLevelMapper.Map(match.Groups[3].Value);
			var callingMethod = match.Groups[4].Value.Trim();
			var message = match.Groups[5].Value;

			entry = new LogEntry
			{
				Date = date,
				Time = time,
				LogLevel = logLevel,
				CallingMethod = callingMethod,
				Message = message
			};
			return true;
		}
	}
}