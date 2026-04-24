using LogTool.Helpers;

namespace LogTool.Services.PrintServices
{
    public abstract class BasePrintService : IPrintService
    {
        /// <summary>
        /// Generic message formatter
        /// </summary>
        /// <param name="args">the parsed arguments</param>
        /// <param name="logFileData">the parsed log file information</param>
        /// <returns>formatted message</returns>
        protected IEnumerable<string> FormatMessage(Arguments args, LogFileData logFileData)
        {
            yield return "Log Analysis Summary";
            yield return "-------------------";
            yield return string.Empty;
            yield return $"Total Lines: {logFileData.NumLines}";
            foreach (var (name, count) in logFileData.LevelCount)
            {
                yield return $"Total {name}: {count}";
            }
            
            if (logFileData.MessageCount.Count > 0)
            {
                yield return string.Empty;
                yield return $"Top {args.Level}s:";
                foreach (var (error, count) in logFileData.MessageCount.OrderByDescending(kvp => kvp.Value).Take(args.NumMessageCount))
                {
                    yield return $"- {error}: {count}";
                }
            }
        }

        public abstract void Print(Arguments args, LogFileData logFileData);
    }
}