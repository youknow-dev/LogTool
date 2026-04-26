namespace LogTool.Models
{
    public record ParseResults(
        bool Success,
        Arguments? Arguments,
        string ErrorMessage,
        bool DisplayHelp
    );
}