namespace LogTool.Helpers
{
    public class FilePrintService : IPrintService
    {
        private readonly string outputPath;

        public FilePrintService(string outputPath)
        {
            this.outputPath = outputPath;
        }

        public void Print(Arguments args, LogFileData logFileData)
        {
            using var writer = new StreamWriter(outputPath);
            writer.WriteLine("Log Analysis Summary");
            writer.WriteLine("-------------------");
            writer.WriteLine();
            writer.WriteLine($"Total Lines: {logFileData.NumLines}");
            foreach (var (name, count) in logFileData.LevelCount)
            {
                writer.WriteLine($"Total {name}: {count}");
            }
            
            if (logFileData.MessageCount.Count > 0)
            {
                writer.WriteLine();
                writer.WriteLine("Top Errors:");
                foreach (var (error, count) in logFileData.MessageCount.OrderByDescending(kvp => kvp.Value).Take(args.NumErrorOutput))
                {
                    writer.WriteLine($"- {error}: {count}");
                }
            }
        }
    }
}