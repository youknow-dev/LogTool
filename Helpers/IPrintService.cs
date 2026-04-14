namespace LogTool.Helpers
{
    public interface IPrintService
    {
        void Print(
            Arguments args,
            Dictionary<string, int> errorCounts,
            Dictionary<string, int> levels,
            int lineCount);
    }
}