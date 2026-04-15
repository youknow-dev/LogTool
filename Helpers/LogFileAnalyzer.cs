namespace LogTool.Helpers
{
    public record LogFileData(
        Dictionary<string, int> LevelCount,
        Dictionary<string, int> MessageCount,
        int NumLines
    );

    public class LogFileAnalyzer
    {
        private readonly HashSet<string> registeredLevels = [];

        public void RegisterLevel(string level)
        {
            registeredLevels.Add(level);
        }

        private Dictionary<string, int> BuildLevelDictionary()
        {
            Dictionary<string, int> levels = [];
            foreach (var level in registeredLevels)
            {
                levels.Add(level, 0);
            }
            return levels;
        }

        private static void IncrementLevel(Dictionary<string, int> levelDict, string key)
        {
            if (levelDict.TryGetValue(key, out int count))
            {
                levelDict[key] = count + 1;
            }
            else levelDict.Add(key, 1);
        }

        public LogFileData Analyze(string logFilePath, string targetLevel)
        {
            if (!registeredLevels.Contains(targetLevel))
                throw new ArgumentException("Target level is not registered.", nameof(targetLevel));

            var levels = BuildLevelDictionary();
            var messages = new Dictionary<string, int>();            

            int msgIndex;
            int numLines = 0;

            foreach (var line in File.ReadLines(logFilePath))
            {
                numLines++;

                var parts = line.Split();

                if (parts.Length >= 4)
                {   
                    var level = parts[2].Trim();

                    if (!registeredLevels.Contains(level))
                        continue;

                    levels[level]++;
                    if (level == targetLevel)
                    {
                        msgIndex = line.IndexOf(level);
                        IncrementLevel(messages, line.Substring(msgIndex + level.Length).Trim());
                    }
                }
            }

            return new LogFileData(
                LevelCount: levels,
                MessageCount: messages,
                NumLines: numLines
                );
        }
    }
}