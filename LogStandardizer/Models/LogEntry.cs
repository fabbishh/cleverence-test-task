namespace CleverenceTestTasks.LogStandardizer.Models
{
	public class LogEntry
	{
		public DateTime Date { get; set; }
		public string Time { get; set; }
		public LogLevel LogLevel { get; set; }
		public string CallingMethod { get; set; } = "DEFAULT";
		public string Message { get; set; }
	}
}
