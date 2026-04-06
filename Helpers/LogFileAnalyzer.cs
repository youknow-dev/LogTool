namespace LogTool.Helpers
{
    public class LogFileAnalyzer
    {
        private Dictionary<string, int> levels;

        private readonly string ErrorKey = "ERROR";

        public LogFileAnalyzer()
        {
            levels = new Dictionary<string, int>()
            {
                {ErrorKey, 0},
                {"WARNING", 0},
                {"INFO", 0}
            };
        }
        
        private static void UpdateDict(Dictionary<string, int> dict, string key)
        {
            if (dict.ContainsKey(key))
            {
                dict[key]++;
            }
            else
            {
                dict.Add(key, 1);
            }
        }

        public (Dictionary<string, int> levels, int lineCount) Analyze(string logpath, Dictionary<string, int> details)
        {
            int msgIndex = 0;
            int numLines = 0;
            foreach (var line in File.ReadLines(logpath))
            {
                numLines++;

                var parts = line.Split();

                foreach (var (level, _) in levels)
                {
                    if (parts[2].Trim() == level)
                    {
                        levels[level]++;
                
                        if (level == ErrorKey) UpdateDict(details, line.Substring(msgIndex).Trim());
                    }
                }
            }

            return (levels, numLines);
        }
    }
}