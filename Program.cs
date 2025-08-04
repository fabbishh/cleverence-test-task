using CleverenceTestTasks.LogStandardizer;
using CleverenceTestTasks.StringCompression;
using CleverenceTestTasks.ThreadSafeCounter;

var stringCompressor = new StringCompressor();
var logParser = new LogParser();
var logStandardizer = new LogStandardizer();

while (true)
{
	Console.WriteLine("Interview Tasks Application");
	Console.WriteLine("\nMenu:");
	Console.WriteLine("1. Task 1: String Compression");
	Console.WriteLine("2. Task 2: Thread-Safe Counter");
	Console.WriteLine("3. Task 3: Log Standardization");
	Console.WriteLine("0. Exit");
	Console.Write("Select a task: ");

	if (int.TryParse(Console.ReadLine(), out int choice))
	{
		Console.Clear();

		switch (choice)
		{
			case 1:
				DemoStringCompression(stringCompressor);
				break;
			case 2:
				DemoThreadSafeCounter();
				break;
			case 3:
				DemoLogStandardization(logStandardizer);
				break;
			case 0:
				return;
			default:
				continue;
		}
	}
	else
	{
		Console.Clear();
		continue;
	}

	Console.WriteLine("\nPress any key to continue...");
	Console.ReadKey();
	Console.Clear();
}

static void DemoStringCompression(StringCompressor compressor)
{
	Console.WriteLine("Task 1: String Compression");

	Console.Write("Enter a string to compress: ");
	string? input = Console.ReadLine();

	try
	{
		string? compressed = compressor.Compress(input);
		string? decompressed = compressor.Decompress(compressed);

		Console.WriteLine($"""
			Original: "{input}"
			Compressed: "{compressed}"
			Decompressed: "{decompressed}"
			""");
	}
	catch (Exception ex)
	{
		DisplayError($"Error processing files: {ex.Message}");
	}

}

static void DemoThreadSafeCounter()
{
	Console.WriteLine("Task 2: Counter Server");

	CounterServer.AddToCount(10);
	Console.WriteLine($"Initial value: {CounterServer.GetCount()}");

	var readers = Enumerable.Range(0, 5).Select(i => Task.Run(() =>
	{
		for (int j = 0; j < 3; j++)
		{
			int value = CounterServer.GetCount();
			Console.WriteLine($"Reader Task {Thread.CurrentThread.ManagedThreadId}: Read value {value}");
			Thread.Sleep(100);
		}
	})).ToArray();

	var writers = Enumerable.Range(0, 2).Select(i =>
	{
		int increment = i + 1;
		return Task.Run(() =>
		{
			for (int j = 0; j < 3; j++)
			{
				CounterServer.AddToCount(increment);
				Console.WriteLine($"Writer Task {Thread.CurrentThread.ManagedThreadId}: Added {increment}");
				Thread.Sleep(150);
			}
		});
	}).ToArray();

	Task.WhenAll([.. readers, .. writers]).Wait();

	Console.WriteLine($"Final value: {CounterServer.GetCount()}");
}

static void DemoLogStandardization(LogStandardizer logStandardizer)
{
	Console.WriteLine("Task 3: Log Standardization");

	string inputFilePath = CleverenceTestTasks.AppConfig.LogInputFilePath;
	string outputFilePath = CleverenceTestTasks.AppConfig.LogOutputFilePath;
	string problemsFilePath = CleverenceTestTasks.AppConfig.LogProblemsFilePath;

	try
	{
		logStandardizer.StandardizeLogs(inputFilePath, outputFilePath, problemsFilePath);
		Console.WriteLine("Processing completed.");
		Console.WriteLine($"Standardized logs written to: {outputFilePath}");
		Console.WriteLine($"Problem lines written to: {problemsFilePath}");
	}
	catch (Exception ex)
	{
		DisplayError($"Error processing files: {ex.Message}");
	}
}

static void DisplayError(string message)
{
	Console.ForegroundColor = ConsoleColor.Red;
	Console.WriteLine(message);
	Console.ResetColor();
}