using LogTool.Helpers;
using LogTool.Models;

namespace LogTool.Services.PrintServices
{
    public interface IPrintService
    {
        void Print(
            Arguments args,
            LogFileData logFileData);
    }
}