using LogTool.Helpers;

namespace LogTool.Services.PrintServices
{
    public interface IPrintService
    {
        void Print(
            Arguments args,
            LogFileData logFileData);
    }
}