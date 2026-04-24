using LogTool.Helpers;

namespace LogTool.Services.PrintServices
{
    public class TerminalPrintService : BasePrintService
    {
        public override void Print(Arguments args, LogFileData logFileData)
        {
            foreach (var line in FormatMessage(args, logFileData))
            {
                Console.WriteLine(line);
            }
        }
    }
}