using LogTool.Helpers;

namespace LogTool
{
    public class Program
    {
        private static void PrintHelp()
        {
            Console.WriteLine("--help:\tdisplay help information");
            Console.WriteLine("--file:\tenter the log file to parse");
            Console.WriteLine("--top:\tset the number of most common errors to display");
            Console.WriteLine("--output:\tset the output type");  
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
                var logpath = parseResults.Arguments?.Files.FirstOrDefault();

                if (File.Exists(logpath))
                {
                    // analyze file
                    var logData = new LogFileAnalyzer().Analyze(logpath, parseResults.Arguments?.Level);

                    // print report to terminal
                    parseResults.Arguments?.OutputType.Print(parseResults.Arguments, logData);
                }
                else
                {
                    Console.WriteLine($"Failed to find log file: '{logpath}'");
                    return 2;
                }
            }
            else
            {
                Console.WriteLine("Parse Error:");
                Console.WriteLine(parseResults.ErrorMessage);   
                return 1;
            }

            return 0;
        }
    }
}