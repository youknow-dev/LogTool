namespace LogTool.Helpers
{
    public class TerminalPrintService : IPrintService
    {
        public void Print(Arguments args, Dictionary<string, int> errorCounts, Dictionary<string, int> levels, int lineCount)
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
                foreach (var (error, count) in errorCounts.OrderByDescending(kvp => kvp.Value).Take(args.NumErrorOutput))
                {
                    Console.WriteLine($"- {error}: {count}");
                }
            }
            
        }
    }
}