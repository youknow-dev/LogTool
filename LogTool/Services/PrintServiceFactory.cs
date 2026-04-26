using LogTool.Services.PrintServices;

namespace LogTool.Services
{
    public enum OutputType
    {
        Console = 1,
        Text = 2,
        Json = 4,
        Xml = 8
    }

    /// <summary>
    /// Factory for the creating new instance of <see cref="IPrintService"/> 
    /// </summary>
    public class PrintServiceFactory
    {
        private readonly Dictionary<OutputType, Func<string, IPrintService>> factory = new()
        {
          {OutputType.Console, (target) => new TerminalPrintService()},
          {OutputType.Text, (target) => new FilePrintService(target)},
          {OutputType.Json, (target) => throw new NotImplementedException()},
          {OutputType.Xml, (target) => throw new NotImplementedException()},
        };

        /// <summary>
        /// Create a new print service based on the desired target
        /// </summary>
        /// <param name="targetType">the location to direct print toward</param>
        /// <param name="targetPath">the path to the file</param>
        /// <returns>The print service</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public IPrintService Create(OutputType targetType, string targetPath = "")
        {
            if (factory.TryGetValue(targetType, out var create))
            {
                return create(targetPath);
            }
            
            string errMessage = $"Print service factory does not contain the following type: '{targetType}'";
            throw new InvalidOperationException(errMessage);
        }
    }
}