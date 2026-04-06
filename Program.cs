using System;
using LogTool.Helpers;

namespace LogTool
{
    public class Program
    {
        private static void PrintErrorInformation(Dictionary<string, int> errorCounts, Dictionary<string, int> levels, int lineCount)
        {
            Console.WriteLine("Log Analysis Summary");
            Console.WriteLine("-------------------");
            Console.WriteLine();
            Console.WriteLine($"Total Lines: {lineCount}");
            foreach (var (name, count) in levels)
            {
                Console.WriteLine($"Total {name}: {count}");
            }
            
            if (errorCounts.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Top Errors:");
                foreach (var (error, count) in errorCounts.OrderByDescending(kvp => kvp.Value).Take(10))
                {
                    Console.WriteLine($"- {error}: {count}");
                }
            }
        }

        public static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Not enough input arguments");
                return 1;
            }

            var logpath = args[0];

            if (File.Exists(logpath))
            {
                // analyze file
                var errorCounts = new Dictionary<string, int>();
                var (levels, lineCount) = new LogFileAnalyzer().Analyze(logpath, errorCounts);

                // print report to terminal
                PrintErrorInformation(errorCounts, levels, lineCount);
            }
            else
            {
                Console.WriteLine($"Failed to find log file: '{logpath}'");
                return 2;
            }

            return 0;
        }
    }
}