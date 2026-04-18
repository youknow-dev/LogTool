using LogTool.Helpers;

namespace LogTool.Services.PrintServices
{
    public class TerminalPrintService : IPrintService
    {
        public void Print(Arguments args, LogFileData logFileData)
        {
            Console.WriteLine("Log Analysis Summary");
            Console.WriteLine("-------------------");
            Console.WriteLine();
            Console.WriteLine($"Total Lines: {logFileData.NumLines}");
            foreach (var (name, count) in logFileData.LevelCount)
            {
                Console.WriteLine($"Total {name}: {count}");
            }
            
            if (logFileData.MessageCount.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine($"Top {args.Level}s:");
                foreach (var (error, count) in logFileData.MessageCount.OrderByDescending(kvp => kvp.Value).Take(args.NumMessageCount))
                {
                    Console.WriteLine($"- {error}: {count}");
                }
            }
        }
    }
}