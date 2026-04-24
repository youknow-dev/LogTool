using LogTool.Helpers;

namespace LogTool.Services.PrintServices
{
    public class FilePrintService : BasePrintService
    {
        private readonly string outputPath;

        public FilePrintService(string outputPath)
        {
            this.outputPath = outputPath;
        }

        public override void Print(Arguments args, LogFileData logFileData)
        {
            using var writer = new StreamWriter(outputPath);
            foreach (var line in FormatMessage(args, logFileData))
            {
                writer.WriteLine(line);
            }
        }
    }
}