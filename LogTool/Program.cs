using LogTool.Helpers;

namespace LogTool
{
    public class Program
    {
        private static void PrintHelp()
        {
            Console.WriteLine("Description:");
            Console.WriteLine("  Scan log file for most prominent logs by level");
            Console.WriteLine();
            Console.WriteLine("Usage:");
            Console.WriteLine("  .\\LogTool.exe [<filepath>] [options]");
            Console.WriteLine();
            Console.WriteLine("Arguments:");
            Console.WriteLine("  <filepath>\tthe path to the log file");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("  --top <n>\t\t\t\tset the number of most common errors to display");
            Console.WriteLine("  --level <ERROR | WARNING | INFO>\tset the information level to print");
            Console.WriteLine("  --output <type | filepath>:\t\tset the output type for the log file information");
            Console.WriteLine("  --help\t\t\t\tdisplay help information");
            Console.WriteLine();
        }

        public static int Main(string[] args)
        {
            var parseResults = new ArgumentParser().Parse(args);
            
            if (parseResults.DisplayHelp)
            {
                PrintHelp();
            }
            else if (parseResults.Success)
            {
                var arguments = parseResults.Arguments;
                var logpath = arguments?.Files.FirstOrDefault();

                if (File.Exists(logpath))
                {
                    // analyze file
                    var logData = new LogFileAnalyzer().Analyze(logpath, arguments!.Level);

                    // print report to terminal
                    arguments!.OutputType.Print(arguments, logData);
                }
                else
                {
                    Console.WriteLine($"Failed to find log file: '{logpath}'");
                    return 2;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Parse Error:");
                Console.WriteLine($"  {parseResults.ErrorMessage}"); 
                Console.WriteLine();
                Console.ResetColor(); // probably not needed
                return 1;
            }

            return 0;
        }
    }
}