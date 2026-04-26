using LogTool.Services;

namespace LogTool.Models
{
    public record Arguments(
        IEnumerable<string> Files,
        int NumMessageCount,
        OutputType OutputType,
        string? OutputPath,
        ErrorLevel Level
    );
}