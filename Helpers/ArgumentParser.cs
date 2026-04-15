namespace LogTool.Helpers
{
    public record Arguments(
        IEnumerable<string> Files,
        int NumMessageCount,
        IPrintService OutputType,
        string Level
    );

    public record ParseResults(
        bool Success,
        Arguments? Arguments,
        string ErrorMessage,
        bool DisplayHelp
    );

    public class ArgumentParser
    {
        private ParseResults ReturnErrorResults(string tokenName, string? arg = null)
        {
            string message = $"failed to parse {tokenName} value{(arg is null ? string.Empty : $": {arg}")}";
            return new ParseResults(false, null, message, false);
        }

        public ParseResults Parse(string[] args)
        {
            if (args.Length < 1)
                return new ParseResults(false, null, "Not enough input arguments", false);

            var files = new List<string>();
            int top = 10;
            string level = "ERROR";
            IPrintService outputType = new TerminalPrintService(); // defaulted

            int i = 0;
            string token;
            while (i < args.Length)
            {
                token = args[i];

                if (token.StartsWith("--"))
                {
                    switch (token)
                    {
                        case "--level":
                            i++;
                            string temp = args[i].ToUpper(); 
                            if (i < args.Length && (temp == "ERROR" || temp == "INFO" || temp == "WARNING"))
                            {
                                level = temp;
                                break;
                            }
                            else return ReturnErrorResults("--level", i < args.Length ? args[i] : null);
                        case "--output":
                            i++;
                            if (i < args.Length && (args[i] == "console" || Path.HasExtension(args[i])))
                            {
                                if (args[i] == "console")
                                {
                                    outputType = new TerminalPrintService();
                                }
                                else
                                {
                                    outputType = new FilePrintService(args[i]);
                                }
                                break;
                            }
                            else return ReturnErrorResults("--output", i < args.Length ? args[i] : null);
                        case "--top":
                            i++;
                            if (i < args.Length && int.TryParse(args[i], out int val))
                            {
                                if (val > 0)
                                {
                                    top = val;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.Write($"incoming val ({val}) for top is an invalid entry. Current value for top: ");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"{top}");
                                    Console.WriteLine("");
                                    Console.ResetColor();
                                }
                                break;
                            }
                            else return ReturnErrorResults("--top", i < args.Length ? args[i] : null);
                        case "--help":
                            return new ParseResults(true, null, string.Empty, true);
                        default:
                            return new ParseResults(false, null, $"unknown flag detected: {args[i]}", false);
                    }
                }
                else files.Add(token);
                i++;
            }

            if (files.Count == 0)
                return new ParseResults(false, null, "No log path entered. Aborting analysis.", false);

            return new ParseResults(
                true,
                new Arguments(
                    files,
                    top,
                    outputType,
                    level),
                string.Empty,
                false
            );
        }
    }
}