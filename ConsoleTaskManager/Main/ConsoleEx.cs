using System;

namespace Main
{
	public static class ConsoleEx
	{
		public static void Write(string a, ConsoleColor color)
		{
			Console.ForegroundColor = color;
			Console.Write(a);
			Console.ResetColor();
		}

		public static void WriteLine(string a, ConsoleColor color)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(a);
			Console.ResetColor();
		}


	}
}
