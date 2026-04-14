namespace LogTool.Helpers
{
    public class FilePrintService : IPrintService
    {
        private readonly string outputPath;

        public FilePrintService(string outputPath)
        {
            this.outputPath = outputPath;
        }

        public void Print(Arguments args, Dictionary<string, int> errorCounts, Dictionary<string, int> levels, int lineCount)
        {
            using var writer = new StreamWriter(outputPath);
            writer.WriteLine("Log Analysis Summary");
            writer.WriteLine("-------------------");
            writer.WriteLine();
            writer.WriteLine($"Total Lines: {lineCount}");
            foreach (var (name, count) in levels)
            {
                writer.WriteLine($"Total {name}: {count}");
            }
            
            if (errorCounts.Count > 0)
            {
                writer.WriteLine();
                writer.WriteLine("Top Errors:");
                foreach (var (error, count) in errorCounts.OrderByDescending(kvp => kvp.Value).Take(args.NumErrorOutput))
                {
                    writer.WriteLine($"- {error}: {count}");
                }
            }
        }
    }
}