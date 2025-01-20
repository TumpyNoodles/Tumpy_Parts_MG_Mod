using System;
using System.IO;

namespace Tumpy_Debug
{
	//LOGGING TO MOD'S OWN FILE
	public class Log
	{
		public Log(string s, bool erase)
		{
			if (!erase)
				File.AppendAllText(file, $"{s}\r\n");
			else
			{
				if (!Directory.Exists(dir))
					Directory.CreateDirectory(dir);
				File.WriteAllText(file, $"{s}\r\n");
			}
		}

		public Log(string s)
		{
			File.AppendAllText(file, $"{s}\r\n");
		}

		public Log()
		{
			File.AppendAllText(file, "\r\n");
		}

		private static string dir = $@"{AppDomain.CurrentDomain.BaseDirectory}\Mods\Logs";
		private static string file = $@"{dir}\Tumpy_Parts.log";
	}

	//VERBOSE LOGGING TO SAME FILE. IT WOULD BE BETTER TO DO LOG LEVELS BUT I'M LAZY AND I'M TRYING TO TYPE AS LITTLE AS POSSIBLE WHEN CREATING LOG ENTRIES
	public class LogV
	{
		public LogV(string s)
		{
			if (verboseLogging)
				File.AppendAllText(file, $"{s}\r\n");
		}

		public static bool verboseLogging = false;
		private static string dir = $@"{AppDomain.CurrentDomain.BaseDirectory}\Mods\Logs";
		private static string file = $@"{dir}\Tumpy_Parts.log";
	}
}
