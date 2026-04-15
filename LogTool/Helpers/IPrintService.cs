namespace LogTool.Helpers
{
    public interface IPrintService
    {
        void Print(
            Arguments args,
            LogFileData logFileData);
    }
}