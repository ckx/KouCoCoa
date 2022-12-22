using System;
using System.IO;
using System.Threading.Tasks;

namespace KouCoCoa {
    internal static class Logger 
    
    {
        public static string Timestamp {
            get {
                return $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
            }
        }

        public static string FullLog { get; private set; }

        private static string _logPath = "";

        public static async Task CreateLogFile() 
        {
            string logDirectory = "Logs";
            Directory.CreateDirectory(logDirectory);
            string logFileName = $"[{DateTime.Now:yyyy-MM-dd}]KouCoCoa-{DateTime.Now:HHmmss}.log";
            _logPath = Path.Combine(logDirectory, logFileName);
            try {
                using (StreamWriter sw = File.CreateText(_logPath)) {
                    await sw.WriteLineAsync($"[{Timestamp}] [KouKou] おはよう、 お兄ちゃん! こうこう、頑張ります！!");
                }
            } catch (Exception) {
                throw;
            }
            Console.WriteLine($"Log file initiated at {_logPath}");
        }

        public static async Task WriteLineAsync(string logMessage, LogLevel logLevel = LogLevel.Info) 
        {
            if (Globals.RunConfig.SilenceLogger) {
                return;
            }
            string line;
            line = $"[{Timestamp}] [{logLevel}] {logMessage}";
            await CommitLogAsync(line, logLevel);
        }

        private static async Task CommitLogAsync(string line, LogLevel logLevel) 
        {
            // Output log only if we are at an appropriate loglevel
            if (Globals.RunConfig.LoggingLevel >= logLevel) {
                Console.WriteLine(line);
                try {
                    using (StreamWriter sw = File.AppendText(_logPath)) {
                        await sw.WriteLineAsync(line);
                    }
                } catch (Exception) {
                    throw;
                }
            }
        }

        public static void WriteLine(string logMessage, LogLevel logLevel = LogLevel.Info)
        {
            if (Globals.RunConfig.SilenceLogger) {
                return;
            }
            string line;
            line = $"[{Timestamp}] [{logLevel}] {logMessage}";
            CommitLog(line, logLevel);
        }

        private static void CommitLog(string line, LogLevel logLevel)
        {
            // Output log only if we are at an appropriate loglevel
            if (Globals.RunConfig.LoggingLevel >= logLevel) {
                FullLog += $"{line}{Environment.NewLine}";
                Console.WriteLine(line);
                if (MainContainer.LogConsoleInitialized) {
                    MainContainer.LogConsole.AddLogLine(line);
                }
                try {
                    using (StreamWriter sw = File.AppendText(_logPath)) {
                        sw.WriteLine(line);
                    }
                } catch (Exception) {
                    throw;
                }
            }
        }
    }
}
