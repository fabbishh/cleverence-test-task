using CleverenceTestTasks.LogStandardizer.Models;
using CleverenceTestTasks.LogStandardizer.Utils;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CleverenceTestTasks.LogStandardizer.Parsers
{
	public class Format1Parser : ILogFormatParser
	{
		private static readonly Regex Regex = new(
			@"^(\d{2}\.\d{2}\.\d{4})\s+(\d{2}:\d{2}:\d{2}\.\d+)\s+(INFORMATION|WARNING|ERROR|DEBUG)\s+(.+)$",
			RegexOptions.Compiled);

		public bool TryParse(string logLine, out LogEntry? entry)
		{
			entry = null;
			if (string.IsNullOrEmpty(logLine))
				return false;

			var match = Regex.Match(logLine);
			if (!match.Success) 
				return false;

			var date = DateTime.ParseExact(match.Groups[1].Value, "dd.MM.yyyy", CultureInfo.InvariantCulture);
			var time = match.Groups[2].Value;
			var logLevel = LogLevelMapper.Map(match.Groups[3].Value);
			var message = match.Groups[4].Value;

			entry = new LogEntry
			{
				Date = date,
				Time = time,
				LogLevel = logLevel,
				Message = message
			};
			return true;
		}
	}
}
