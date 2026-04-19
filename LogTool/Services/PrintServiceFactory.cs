using LogTool.Services.PrintServices;

namespace LogTool.Services
{
    /// <summary>
    /// Factory for the creating new instance of <see cref="IPrintService"/> 
    /// </summary>
    public class PrintServiceFactory
    {
        private readonly Dictionary<string, Func<string, IPrintService>> factory = new()
        {
          {"console", (target) => new TerminalPrintService()},
          {"file", (target) => new FilePrintService(target)}, // default
        };

        /// <summary>
        /// Create a new print service based on the desired target
        /// </summary>
        /// <param name="target">the location to direct print toward</param>
        /// <returns>The print service</returns>
        public IPrintService Create(string target)
        {
            if (factory.TryGetValue(target, out var create))
            {
                return create(target);
            }
            
            return factory["file"](target); // default
        }
    }
}