using System.Text;
using System.Text.RegularExpressions;

namespace CleverenceTestTasks.StringCompression
{
	public class StringCompressor
	{
		public string? Compress(string? input)
		{
			if (string.IsNullOrEmpty(input))
				return input;

			if (!Regex.IsMatch(input, @"^[a-z]+$"))
			{
				throw new ArgumentException("Input must contain only lowercase Latin letters (a-z).");
			}

			var result = new StringBuilder();
			char currentChar = input[0];
			int count = 1;

			for (int i = 1; i < input.Length; i++)
			{
				if (input[i] == currentChar)
				{
					count++;
				}
				else
				{
					if (count == 1)
						result.Append(currentChar);
					else
						result.Append(currentChar).Append(count);

					currentChar = input[i];
					count = 1;
				}
			}

			if (count == 1)
				result.Append(currentChar);
			else
				result.Append(currentChar).Append(count);

			return result.ToString();
		}

		public string? Decompress(string? compressedInput)
		{
			if (string.IsNullOrEmpty(compressedInput))
				return compressedInput;

			var result = new StringBuilder();
			int i = 0;

			while (i < compressedInput.Length)
			{
				char currentChar = compressedInput[i++];

				int start = i;
				while (i < compressedInput.Length && char.IsDigit(compressedInput[i]))
					i++;

				int count = 1;
				if (start < i)
				{
					count = int.Parse(compressedInput.Substring(start, i - start));
					if (count <= 0)
						throw new ArgumentException($"Invalid repeat count: {count} for character '{currentChar}'");
				}

				result.Append(new string(currentChar, count));
			}

			return result.ToString();
		}
	}
}
