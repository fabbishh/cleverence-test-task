using System;
using System.IO;
using System.Text;
using CleverenceTestTasks.LogStandardizer.Models;

namespace CleverenceTestTasks.LogStandardizer
{
	public class LogStandardizer
	{
		private readonly LogParser _parser = new LogParser();

		public void StandardizeLogs(string inputFilePath, string outputFilePath, string problemsFilePath)
		{
			try
			{
				using var inputReader = new StreamReader(inputFilePath, Encoding.UTF8);
				using var outputWriter = new StreamWriter(outputFilePath, false, Encoding.UTF8);
				using var problemsWriter = new StreamWriter(problemsFilePath, false, Encoding.UTF8);

				string line;
				while ((line = inputReader.ReadLine()) != null)
				{
					var isValid = _parser.TryParse(line, out var entry);

					if (isValid)
					{
						var standardizedLine = FormatLogEntry(entry!);
						outputWriter.WriteLine(standardizedLine);
					}
					else
					{
						problemsWriter.WriteLine(line);
					}
				}
			}
			catch (FileNotFoundException ex)
			{
				Console.Error.WriteLine($"File not found: {ex.FileName}");
			}
			catch (UnauthorizedAccessException ex)
			{
				Console.Error.WriteLine($"Access denied: {ex.Message}");
			}
			catch (IOException ex)
			{
				Console.Error.WriteLine($"I/O error: {ex.Message}");
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"Unknown error: {ex.Message}");
			}
		}

		private string FormatLogEntry(LogEntry entry)
		{
			var formattedDate = entry.Date.ToString("dd-MM-yyyy");

			return $"{formattedDate}\t{entry.Time}\t{entry.LogLevel}\t{entry.CallingMethod}\t{entry.Message}";
		}
	}
}
